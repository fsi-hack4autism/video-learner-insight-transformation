# Video Learner Insights (Use Case 2)

## Team Split 

1. LOGIN: Create a login component that allows a user (parent, therapist, etc...) to login into the application. This can be through social media, or through AAD. Incorporate security best practices. Store all user data into a database. 

2. UPLOAD: Be able to upload a video file into an application. Store multiple videos along with their metadata into a database. View all the videos on the front end by session date. Optional: organize by month, year.

3. INTERACTION: Add functionality to comment at different timestamps of a given video. (Ex: 0:23 - Penny smiled when the toy was introduced.) Allow users to comment in real time using ACS. For each video, have a place to write down the session goals. Store all of this data in a database.

4. INSIGHTS: Allow users to view a transcript from the session video using Speech to Text. Extract insights of key words and sentiment on the video using Language services from Cognitive Services. In addition, create a summary of each session.

5. DOCUMENT: Create an architecture diagram of the Azure technologies we will use and document how each step connects with one another in our Github page.

## Technology

 - Azure AD B2C
 - Speech to Text
 - Azure Cogntiive Services
 - Sharepoint on Stream
 - Azure SQL
 - [Azure Functions](https://learn.microsoft.com/en-us/azure/azure-functions/)

## Pre-reqs
1. [VS Code](https://github.com/devanshithakar12/WhatTheHack/blob/xxx-SpeechToText/000-HowToHack/WTH-Common-Prerequisites.md#visual-studio-code)

## Resources
* LOGIN
1. [Sign in using AAD B2C](https://learn.microsoft.com/en-us/azure/active-directory-b2c/quickstart-single-page-app)
* INTERACTION 
1. [Overview of Stream on Sharepoint](https://learn.microsoft.com/en-us/stream/streamnew/new-stream)
2. [Video Portal with Stream on Sharepoint](https://learn.microsoft.com/en-us/stream/streamnew/portals-guide-video-portal#general-setup)
3. [Video Portal with JSON formatted library](https://medium.com/@anand.vadivelan/creating-a-video-portal-in-sharepoint-with-json-formatted-document-library-e886209159ff)

* INSIGHTS
1. [Video Indexer API](https://api-portal.videoindexer.ai/)
2. [Video Indexer Output](https://learn.microsoft.com/en-us/azure/azure-video-indexer/video-indexer-output-json-v2)
3. [Azure Speech SDK](https://learn.microsoft.com/en-us/azure/cognitive-services/speech-service/quickstarts/setup-platform?tabs=windows%2Cubuntu%2Cdotnet%2Cjre%2Cmaven%2Cnodejs%2Cmac%2Cpypi&pivots=programming-language-python)
4. [Speech To Text from a file](https://learn.microsoft.com/en-us/azure/cognitive-services/speech-service/how-to-recognize-speech?pivots=programming-language-python)
5. [Key word extraction](https://learn.microsoft.com/en-us/azure/cognitive-services/language-service/key-phrase-extraction/overview)
6. [Sentiment Analysis](https://learn.microsoft.com/en-us/azure/cognitive-services/language-service/sentiment-opinion-mining/overview)
7. [Summarization](https://learn.microsoft.com/en-us/azure/cognitive-services/language-service/summarization/how-to/document-summarization)
