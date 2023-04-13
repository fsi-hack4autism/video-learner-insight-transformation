# Video Learner Insights (Use Case 2)

## Goals
1. Sign In + Upload Video + View all sessions in one place
	- Log In 
	- Take the video that has been recorded 
	- Push the video to a repository 

2. Interaction with BCBA and Parents
	- Chat Functionality 
	- Express Reactions at certain parts of time frame in video
	- Be able to see this interaction when you open the video

3. Add AI
	- Speech to Text (create a transcription)
	- Sentiment Analysis 
	- Extract insights from the patient's video repo - eye signals, key words being spoken 


## Team Split 

1. LOGIN: Create a login component that allows a user (parent, therapist, etc...) to login into the application. This can be through social media, or through AAD. Incorporate security best practices. Store all user data into a database. 

2. UPLOAD: Be able to upload a video file into an application. Store multiple videos along with their metadata into a database. View all the videos on the front end by session date. Optional: organize by month, year.

3. INTERACTION: Add functionality to comment at different timestamps of a given video. (Ex: 0:23 - Penny smiled when the toy was introduced.) Allow users to comment in real time using ACS. Store all of this data in a database.

4. INSIGHTS: Allow users to view a transcript from the session video using Speech to Text. In addition, extract insights of key words and sentiment on the video using Language services from Cognitive Services. 

5. DOCUMENT: Create an architecture diagram of the Azure technologies we will use and document how each step connects with one another in our Github page.

## Technology

 - Azure AD B2C
 - Speech to Text
 - Azure Cogntiive Services
 - Sharepoint on Stream
 - Azure SQL
 - [Azure Functions](https://learn.microsoft.com/en-us/azure/azure-functions/)
