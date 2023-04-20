---
page_type: sample
languages:
- javascript
- typescript
- nodejs
name: "JavaScript end-to-end client file upload to Azure Storage Blobs"
description: "Locally build and deploy client application to an Azure Static Web App with a GitHub action, analyze image with Cognitive Services Computer Vision."
products:
- azure
- azure-storage
- azure-portal
- vs-code
- azure-computer-vision
- azure-app-service-static
---

# JavaScript end-to-end client file upload to Azure Storage Blobs

This sample project is a TypeScript React (create-react-app) framework client app with an HTML form to select a file for upload to Azure Storage Blobs. 

The user:
* selects an image from the file system
* uploads the image to Storage Blobs

* [Read Tutorial](https://docs.microsoft.com/azure/developer/javascript/tutorial/browser-file-upload-azure-storage-blob) - The tutorial demonstrates how to load and run the project locally with VSCode. The tutorial includes creating a Storage resource, SAS token and CORS configuration. 


## Sample application

The React (create-react-app) client app consists of the following elements:

* **React** app hosted on port 3000
* uploadToBlob.ts using **@azure/storage-blob** client library to create Blob container and upload file

## Features

This project framework provides the following features:

* Create Azure Storage resource
* Generate SAS token for Storage resource
* Set Storage resource CORS
* Select and upload file to Azure Storage Blob Container

## Getting Started

1. Clone or download repo. 
1. Create Azure Storage resource - using /scripts/newStorageService.js. This resource name is the `storageAccountName`.
1. Generate SAS Token for Storage resource - using /scripts/az-storage-generte-sas.sh. This value is the `sasToken`.
1. Configure CORS for browser - using /scripts/az-storage-cors-add.sh

    Settings for CORS:
    * Allowed origins: `*`
    * Allowed methods: `DELETE, GET, HEAD, MERGE, POST, OPTIONS, and PUT`
    * Allowed headers: `*`
    * Exposed headers: `*`
    * Max age: `86400`
1. Install dependencies: 

    ```javascript
    npm install
    ```

    To run the React app, you need the following Azure SDK client npm packages:
    * @azure/ms-rest-nodeauth
    * @azure/storage-blob

    A third Azure package, @azure/arm-storage, is listed in the `package.json` strictly for use by the `scripts/newStorageService.js` file to create a new Azure Storage resource.

1. Create a file name `.env` at the root of the project.
1. Add two required variables with their storage values:

    ```text
    REACT_APP_STORAGESASTOKEN=
    REACT_APP_STORAGERESOURCENAME=
    ```

    React builds the static files with these variables.

1. If the token begins with a question mark, remove the `?`. The code file provides the `?` for you so you don't need it in the token.

1. Start project: 

    ```javascript
    npm start
    ```

1. View project in browser, `http://localhost:3000`.

1. Select image then select `Upload!`. 

    Page displays images in container. 

## Prerequisites

- Git, if cloning 
- Node.js and NPM
- Web browser
- Azure subscription to create resource on

## Installation

1. Install the sample's dependencies:

   ```javascript
    npm install
    ```

1. Run the command to run the web app.

    ```javascript
    npm start
    ```

1. Open a web browser and use the following url to view the client app on your local computer.

    ```url
    http://localhost:3000/
    ```

## Troubleshooting

If you received an error or your file doesn't upload to the container, check the following:

* Recreate your SAS token, making sure that your token is created at the Storage resource level and not the container level. Copy the new token into the code at the correct location.
* Check that the token string you copied into the code doesn't contain the `?` (question mark) at the beginning of the string.
* Verify your CORS setting for your Storage resource.

## Additional scripts

* Create Azure Storage Blob from JavaScript file: scripts/newStorageService.js
* Set CORS for service using Azure CLI script: scripts/az-storage-cors-add.sh
* Generate SAS Token using Azure CLI script: scripts/az-storage-generate-sas.sh

## Images

The /images folder includes images for upload. 

## Architecture Diagram
![image](https://user-images.githubusercontent.com/95766933/161324858-076b3dc5-f8fe-4d4d-af82-fd68c9938543.png)


## Team 1 Updates:
2022-MAR-31:

* Set up blob storage account, the logic app, Cosmos DB, and Media Service
* Investigated emailing functionality using the logic app
    * Seemed to have issues with credentials
* Investigated how to setup an action on logic app to upload to SharePoint
    * Ran into an access issue 
* Started to hook up the cosmos db to logic app
    * Kept getting BadRequest error when we tried to run the trigger
* Went through the capabilities of the Video Analyzer
    * We uploaded an example video
    * Couldn't find a premade action on the logic app
    * Next step is to see if we can do it with code!
* Resources>
    *https://docs.microsoft.com/en-us/azure/azure-video-analyzer/video-analyzer-for-media-docs/logic-apps-connector-tutorial       
    
2022-APR-01:

* To get around the connection issues, we ended up using our tech leads' credentials
* We switched to using "Logic App" rather than "Logic App (Standard)" which seems to have worked better for us
* Got logic app to save the blob to SharePoint, save the metadata into Cosmos DB, and send an email
* For now we've hard coded the sentiments into Cosmos DB
* Attempted to use Video Indexer (v2) to get an Access Token and upload the video to video analyzer
    * Got stuck getting the access token due to access issues
    

## Team 2 Updates: Upload functionality 
2022-MAR 31 - 2022-APR-01:
1. Resources used 
    > https://docs.microsoft.com/en-us/azure/developer/javascript/tutorial/browser-file-upload-azure-storage-blob?WT.mc_id=email
    > https://docs.microsoft.com/en-us/azure/cosmos-db/sql/create-sql-api-nodejs
    > https://docs.microsoft.com/en-us/azure/cosmos-db/sql/sql-api-nodejs-get-started
    > iFrame link for AZURE analyzer, sample: 
        HTML :<iframe width="580" height="780" src="https://www.videoindexer.ai/embed/insights/5d6ddf3c-18
     ![image](https://user-images.githubusercontent.com/95766933/161299618-05fdef08-3e76-49ef-bb2b-17e44a23eb52.png)

    
3. Application Link > https://black-tree-08edee810.1.azurestaticapps.net/
4. Testcontainer new resource is created > url : https://usecase2videoanalyzer.blob.core.windows.net/testcontainer
5. Screen Visual/ Image Updates  >
 ![image](https://user-images.githubusercontent.com/95766933/161149290-037acb64-07f9-4b66-ae2e-d31148d7fc69.png)
6. iFrame details > <iframe width="580" height="780" src="https://www.videoindexer.ai/embed/insights/5d6ddf3c-18fb-4cc2-b473-6be397c1b17e/3fa07788a3/?&locale=en&location=trial" frameborder="0" allowfullscreen></iframe>
7. AZURE analyzer tutorial https://docs.microsoft.com/en-us/azure/azure-video-analyzer/video-analyzer-for-media-docs/logic-apps-connector-tutorial  
8. Azure Video Analyzer   > https://docs.microsoft.com/en-us/azure/azure-video-analyzer/video-analyzer-for-media-docs/video-indexer-overview
9. Logic Apps  > https://docs.microsoft.com/en-us/azure/logic-apps/
10.Blob Storage > https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blobs-introduction
11.Cosmos DB  >https://docs.microsoft.com/en-us/learn/modules/work-with-cosmos-db/
12.Microsoft Stream > https://docs.microsoft.com/en-us/stream/streamnew/features-new-version-stream
    >https://docs.microsoft.com/en-us/stream/streamnew/new-stream
13.Azure App Service >https://docs.microsoft.com/en-us/azure/app-service/overview
  

##  Learnings
1. Node.js LTS with NPM
2. Visual Studio Code 
3. Visual Studio Code extensions:
        *Azure Resource
        *Azure Storage - used to view Storage resource
        *Azure Static Web Apps - used to create and deploy the React app to Azure
 4. Azure analyzer
 5. Recommend using a smaller size video for testing.
        
 ## Challenges Faced 
 1. Obtaining LogicApps credentials to give access to all underlying apps. 
 2. Deployment token unavailable , need ADMIN access to repo. 
 3. Static web app connection to storage resource and adding storage secrets.
 4. Sharepoint link related issues that were resolved with help from Devanshi and Pierre.
 5. Email ID's to share the video file is limited to outlook emails?? 
 6. Faced CORS policy error, unable to hit the AZURE COSMOS db. Explored the below link for solution
    > https://docs.microsoft.com/en-us/rest/api/storageservices/cross-origin-resource-sharing--cors--support-for-the-azure-storage-services 
        > NOTE : Currently, you cannot use wildcards as part of the domain name. For example https://*.mydomain.net format is not yet supported. Follow below link for further help  >https://docs.microsoft.com/en-us/azure/cosmos-db/sql/how-to-configure-cross-origin-resource-sharing#:~:text=The%20Core%20(SQL)%20API%20in,the%20rules%20you%20have%20specified.
  7. Creating sharepoint link. Went with embedded link to the webpage > https://microsoft.sharepoint.com/:v:/t/AutismUseCase2/EThoyxxEirZMtozR6Kq-TDkB_m7yU2-Ewaj8JaRPbakxpQ?e=9ueEU6.
  8. Configure the SAS token.


## FUTURE STATE Changes reccomended:
1. Functional:
    * Use Case1 : Multiple client -Seggregate videos/files for individual client so that the relevant files are shared with the right teams , family members, ABA therapists. The currenst state covers only one client where videos uploaded are send to same team of people for review/ commentary , this should be expanded in the future state. Introduce the database to store the patient details and link their videos together and their specific team in the database so the emails are routed to the relevant people.
    * Use Case2 : Multiple videos for single client
    
2. Eliminate all hard code. Sharepoint links etc.
3. Integration of Logic apps to azure analyzer.
4. Interactive dashboard for all the parties involved with the client.
5. Bringing in IOT data to the webapp.
6. Opening the availability to multiple interfaces iOS, andriod etc.   
    

 ## APPENDIX:
 1. COSMOS DB 
    Uri: https://usecase2cosmosdbacct.documents.azure.com:443/
    databaseId: usecase2cosmoscontainer
    containerId: container1
    AccountKey (static) : CuQMVMLbaPMw8Ut72NmaurZShQFocyJd4fa1roQzNtcedWglJRPydKbMfDEsmhNLQPhlsxLaAVaNnxT1JPa2ig==
 2. SQL statements:
    SELECT c.metadata.Name,c.sentiments FROM c
    


