import React, { useState } from 'react';
import Path from 'path';
import uploadFileToBlob, { isStorageConfigured } from '../azure-storage-blob';
import InsightsNavbar from './InsightsNavbar';
const CosmosClient = require("@azure/cosmos").CosmosClient;
const config = require("../config");
const storageConfigured = isStorageConfigured();

const Upload = () => {


    const [blobList, setBlobList] = useState<string[]>([]);
    const { endpoint, key, databaseId, containerId } = config;

    const client = new CosmosClient({ endpoint, key });
    const [itemReturnedFromDb, setItemReturned] = React.useState<any>();
    const database = client.database(databaseId);
    const container = database.container(containerId);

    // current file to upload into container
    const [fileSelected, setFileSelected] = useState(null);

    // UI/form management
    const [uploading, setUploading] = useState(false);
    const [inputKey, setInputKey] = useState(Math.random().toString(36));

    const onFileChange = (event: any) => {
        // capture file into state
        setFileSelected(event.target.files[0]);
    };

    const onFileUpload = async () => {
        // prepare UI
        setUploading(true);

        // *** UPLOAD TO AZURE STORAGE ***
        const blobsInContainer: string[] = await uploadFileToBlob(fileSelected);

        // prepare UI for results
        setBlobList(blobsInContainer);

        // reset state/form
        setFileSelected(null);
        setUploading(false);
        /* Copyright 2022 Bloomberg Finance L.P. */
        if (!uploading) {
            try {
                // <QueryItems>
                console.log(`Querying container: Items`);
                const querySpec = {
                    query: "SELECT c.metadata.Name,c.sentiments FROM c"
                }

                // read all items in the Items container
                const { resources: items } = await container.items
                    .query(querySpec)
                    .fetchAll();

                items.forEach((item: { id: any; description: any; }) => {
                    console.log(item, "item sentiments");
                    setItemReturned(item)
                });
            }
            catch { console.log(itemReturnedFromDb); console.error("db connection failed") }
            setInputKey(Math.random().toString(36));
        };
    }

    // display form
    const DisplayForm = () => (
        <div>
            <input type="file" onChange={onFileChange} key={inputKey || ''} />
            <button className='button' type="submit" onClick={onFileUpload}>
                Upload

            </button>
        </div>
    )

    // display file name and image
    const DisplayImagesFromContainer = () => (
        <div>
            <h2>Videos available</h2>
            <ul className='no-bullets'>
                {blobList.map((item) => {
                    return (
                        <li key={item}>
                            <div className={"file-name"}>
                                {Path.basename(item)}
                                {/* Copyright 2022 Bloomberg Finance L.P.  */}
                                <a className="link-class" href="https://microsoft.sharepoint.com/:v:/t/AutismUseCase2/EThoyxxEirZMtozR6Kq-TDkB_m7yU2-Ewaj8JaRPbakxpQ?e=dsJt7Q">Here is a link to sharepoint</a>
                                <br />
                                <iframe width="580" height="780" src="https://www.videoindexer.ai/embed/insights/5d6ddf3c-18fb-4cc2-b473-6be397c1b17e/3fa07788a3/?&locale=en&location=trial" frameBorder="0" allowFullScreen></iframe>
                                {/* <img src={item} alt={item} height="200" /> */}
                            </div>
                        </li>
                    );
                })}
            </ul>
        </div>
    );


    return (
        <div>
            <InsightsNavbar />
            <div className="bordering-container">
                <h1>Upload learner video to the portal</h1>
                {storageConfigured && !uploading && DisplayForm()}
                {storageConfigured && uploading && <div>Uploading</div>}
                <hr />
                {storageConfigured && blobList.length > 0 && DisplayImagesFromContainer()}
                {!storageConfigured && <div>Storage is not configured.</div>}
            </div>
        </div>
    );

}

export default Upload;