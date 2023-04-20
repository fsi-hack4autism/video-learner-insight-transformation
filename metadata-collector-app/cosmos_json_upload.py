import sys,traceback
from azure.cosmos import CosmosClient
import argparse
import os
import json

URL = os.environ['ACCOUNT_URI']
KEY = os.environ['ACCOUNT_KEY']
client = CosmosClient(URL, credential=KEY)


def upload_json(database,container,json_obj):
    database = client.get_database_client(database)
    container = database.get_container_client(container)
    try:
        container.upsert_item(json_obj)
    except Exception as ex:
        print(traceback.format_exc())
        print('Problem upload json file!')
        sys.exit(-1)

def parseArgs():
    argparser = argparse.ArgumentParser()
    argparser.add_argument('-j','--json',help='json file to upload', type=str,required=True)
    argparser.add_argument('-c', '--container', help='cosmos DB container to upload json to',required=True, type=str)
    argparser.add_argument('-d','--database', help='cosmos DB database name to add to', required=True, type=str)
    return argparser.parse_args()

def main():
    args = parseArgs()
    print(f'Attempting upload of {args.json} to cosmos DB container {args.container} on instance {args.database}')
    upload_json(args.database,args.container,args.json)

if __name__ == '__main__':
    main()