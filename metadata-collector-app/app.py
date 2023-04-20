import os
import json
import requests
import time
from flask import (Flask, redirect, render_template, request,
                   send_from_directory, url_for)
import cosmos_json_upload

app = Flask(__name__)

ACCOUNT_ID = "add-account-id-here"
API_KEY = "add-api-key-here"
HEADERS = {'Ocp-Apim-Subscription-Key': f'{API_KEY}'}

@app.route('/')
def index():
    return "Metadata Collector API"

@app.route("/uploadVideo")
def upoad_video():
    video_url = "add-video-url-here"
    
    get_access_token_url = f"https://api.videoindexer.ai/Auth/trial/Accounts/{ACCOUNT_ID}/AccessTokenWithPermission"
    access_token_response = requests.get(get_access_token_url, headers=HEADERS, params={"permission":"Contributor"})
    accessToken = access_token_response.text.strip('"')

    post_upload_video_url = f"https://api.videoindexer.ai/trial/Accounts/{ACCOUNT_ID}/Videos" 
    upload_video_response = requests.post(post_upload_video_url, headers=HEADERS, params={"videoUrl":video_url, "name":"test4", "accessToken":accessToken})
    response_dict = dict(json.loads(upload_video_response.text))
    video_id = response_dict["id"]
    
    return videoindexer(video_id)


def videoindexer(video_id):    
    # time.sleep(5*60) To let the Azure Video Indexer index the uploaded video
    get_index_url = f"https://api.videoindexer.ai/trial/Accounts/{ACCOUNT_ID}/Videos/{video_id}/Index"
    get_access_token_url = f"https://api.videoindexer.ai/Auth/trial/Accounts/{ACCOUNT_ID}/AccessTokenWithPermission"
    
    access_token_response = requests.get(get_access_token_url, headers=HEADERS, params={"permission":"Contributor"})
    accessToken = access_token_response.text.strip('"')
    
    index_response = requests.get(get_index_url, headers=HEADERS, params={"accessToken":accessToken})
    
    json_obj = json.loads(index_response.text)
    print(index_response.status_code)
    
    cosmos_json_upload.upload_json("conversations","conversations",json_obj)
    
    return index_response.text

if __name__ == '__main__':
    app.run()
