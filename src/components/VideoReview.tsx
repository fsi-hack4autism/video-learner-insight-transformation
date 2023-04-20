import React from 'react';
import InsightsNavbar from "./InsightsNavbar";
import { Button, Col, Container, Row } from "react-bootstrap";
import VideoList from "./VideoList";
import DashboardCard from "./DashboardCard";
import video1 from '../assets/videos/video1.mp4';
import video2 from '../assets/videos/video2.mp4';

import video3 from '../assets/videos/video3.mp4';
import video4 from '../assets/videos/video4.mp4';


import picture from '../assets/images/logo192.png';


const VideoReview = ({...props}) => {


   
//    const videoMap = {
//         "Story": 0,
//         "Cooking": 1,
//         "Video Game": 2,
//         "Music": 3,
//     }

    const videoList = [
        {
            id: {
                videoId: '1'

            },
            src: video1,
            snippet: {
                title: 'Story',
                thumbnails: {
                    medium: {
                        url: picture
                    }   
                },
                tags : ["positive", "learning"],
            }
        },
        {
            id: {
                videoId: '2'

            },  
            src: video2,
    
            snippet: {
                title: 'Cooking',
                thumbnails: {
                    medium: {
                        url: picture
                    }
                },
                "tags" : ["fun", "focus"],
            }

        },
        {
            id: {
                videoId: '3'

            },     
            src: video3,
 
            snippet: {
                title: 'Video Game',
                thumbnails: {
                    medium: {
                        url: picture
                    }
                },
                "tags" : ["social", "happy"],
            }

        },
        {
            id: {
                videoId: '4'

            },      
            src: video4,

            snippet: {
                title: 'Music',
                thumbnails: {
                    medium: {
                        url: picture
                    }
                },
                "tags" : ["calm", "relaxing"],
            }

        }
    ];

    const [videos, setVideos] = React.useState(videoList);

    const videoChange = (video: any) => {
        const index = videos.indexOf(video);
        setVideoIndex(index);
    }


    const [videoIndex, setVideoIndex] = React.useState(0);
    const selectedVideo = videos[videoIndex];


    // TODO: Get video id from url improve this WIP
//   let key = window.location.toString().split("/").pop();
//    if( key !== "undefined" || key !== null ){
//         console.log(key);
//         if(videoMap[key] !== undefined || videoMap[key] !== null){
//             setVideoIndex(videoMap[key]);
//         } else {
//             key = "Story";
//             //alert("Please try again issue with key from video" + key);
//             props.history.push("/video/" + key);
//         }
//    } else {
//         alert("Please try again issue with video" + key);
//         //props.history.push("/dashboard");
//    } 
//    ;

const sortby = () => {
    const vid = videos.sort((a, b) => (a.snippet.title > b.snippet.title) ? 1 : -1);
    setVideos(vid);
    console.log(videos)
    setVideoIndex(videos[0].id.videoId.toString());
}
 

    return (
        <div>
            <InsightsNavbar />
            <h5> Welcome to Videos </h5>
            <Container fluid>
                <Row className="listRow">
                    <Col md="6" className='listColumn'>
                        <h5> Video List </h5> <Button onClick={sortby} style={{ backgroundColor: `var(--green)`, border: 'none'}}> Sort </Button>
                        <VideoList videos={videos} selectedVideo={videos[videoIndex]} onVideoSelect={(video) => videoChange(video)} />
                    </Col>

                    <Col md="6">
                        <DashboardCard title={selectedVideo.snippet.title} video={videos[videoIndex].src} tags={[]} />
                    </Col >
                </Row>
                
            <hr></hr>
            <Row>
            <Col md="6">
                    <h5> Summary</h5>
                    <DashboardCard title={videos[videoIndex].snippet.title} value="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. At tellus at urna condimentum mattis pellentesque id nibh.">
                    </DashboardCard>
                </Col>
                <Col md="6">
                    <h5> Discussion</h5>
                    <DashboardCard comment='Experience distress at 0:05' author='Jack, Therapist' height='20px'></DashboardCard>
                    { (videoIndex !== 1) ?
                        <DashboardCard comment='Experience joy at 2:07' author='David, Parent' height='20px'></DashboardCard>
                        :                     <DashboardCard comment='Experience Unease at 1:05' author='Jack, Parent' height='20px'></DashboardCard>

                    }                   
                </Col>
            </Row>
            </Container>
        </div>

    );
};
export default VideoReview;