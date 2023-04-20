import sys,traceback
import datetime
from azure.cosmos import CosmosClient
import argparse
import os
import json

URL = os.environ['ACCOUNT_URI']
KEY = os.environ['ACCOUNT_KEY']
client = CosmosClient(URL, credential=KEY)

def convert_from_epoch(timestamp):
    return datetime.datetime.fromtimestamp(int(timestamp))

def update_speaker_timestamps(json_dict, indexed_json_dict,json_target,database,container):
    list_videos = json_dict['videos']
    times_and_comments = match_tags_with_users(indexed_json_dict)
    users_found = []
    for vid in list_videos:
        new_speaker_list = []
        for speaker in vid['insights']['speakers']:
            curr_diff = 0
            user_name = ''
            for user_time in times_and_comments:
                time_diff = (datetime.datetime.strptime(speaker['instances'][0]['adjustedStart'][:-3],'%H:%M:%S') - user_time['time']).total_seconds()
                new_diff = abs(time_diff)
                new_user = user_time['name']
                if new_user in users_found:
                    continue
                if new_diff > curr_diff:
                    curr_diff = new_diff
                    user_name = new_user
            users_found.append(user_name)
            print('%s: seems to be user: %s' %(speaker['name'], user_name))
            new_spk = speaker
            new_spk['name'] = user_name
            new_speaker_list.append(new_spk)
        vid['insights']['speakers'] = new_speaker_list
        continue
    upload_json(database,container,json.dumps(json_dict))


def upload_json(database, container, json_file):
    database = client.get_database_client(database)
    container = database.get_container_client(container)
    json_file = json.load(open(json_file))
    try:
        container.upsert_item(json_file)
    except Exception as ex:
        print(traceback.format_exc())
        print('Problem upload json file!')
        sys.exit(-1)

def match_tags_with_users(json_dict):
    users_times = []
    for comment in json_dict['comments']:
        comment_time = convert_from_epoch(comment['timestamp'])
        name = comment['commentText']
        users_times.append({'name':name,'time':comment_time})
    return users_times

def parseArgs():
    argparser = argparse.ArgumentParser()
    argparser.add_argument('-bj','--base_json',help='json file to upload', type=str,required=True)
    argparser.add_argument('-ij','--indexed_json',help='json file with user tags', type=str,required=True)
    argparser.add_argument('-c', '--container', help='cosmos DB container to upload json to',required=True, type=str)
    argparser.add_argument('-d','--database', help='cosmos DB database name to add to', required=True, type=str)
    return argparser.parse_args()

def main():
    args = parseArgs()
    json_src_dict = json.load(open(args.base_json))
    json_target_dict = json.load(open(args.indexed_json))
    speakers = update_speaker_timestamps(json_src_dict,json_target_dict,args.base_json,args.database,args.container)

if __name__ == '__main__':
    main()