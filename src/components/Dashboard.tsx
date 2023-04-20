import React from "react";
import { Container, Row, Col } from 'react-bootstrap';
import '../styles/Dashboard.css';
import DashboardCard from "./DashboardCard";
import InsightsNavbar from "./InsightsNavbar";
import picture from '../assets/images/insights.png';
import graph from '../assets/images/legotherapy.png';
import video1 from '../assets/videos/video1.mp4';
import video2 from '../assets/videos/video2.mp4';
import video3 from '../assets/videos/video3.mp4';
import video4 from '../assets/videos/video4.mp4';


const Dashboard = () => {

  // List of sentiments is analysis of video metadata 
  const sentiments = [
      {
        text: "Empathy",
        value: 47
      },
      {
        text: "Resilience",
        value: 82
      },
      {
        text: "Gratitude",
        value: 16
      },
      {
        text: "Integrity",
        value: 73
      },
      {
        text: "Curiosity",
        value: 41
      },
      {
        text: "Compassion",
        value: 58
      },
      {
        text: "Creativity",
        value: 90
      },
      {
        text: "Honesty",
        value: 29
      },
      {
        text: "Perseverance",
        value: 67
      },
      {
        text: "Adaptability",
        value: 51
      },
      {
        text: "Kindness",
        value: 14
      },
      {
        text: "Open-mindedness",
        value: 77
      },
      {
        text: "Patience",
        value: 36
      },
      {
        text: "Courage",
        value: 95
      },
      {
        text: "Generosity",
        value: 22
      },
      {
        text: "Responsibility",
        value: 62
      },
      {
        text: "Optimism",
        value: 44
      },
      {
        text: "Tolerance",
        value: 88
      },
      {
        text: "Humility",
        value: 7
      },
      {
        text: "Empowerment",
        value: 53
      }
    ];
  

  // userInteractionData is a list of users that have access to the current learner's videos , can show and hide based on role and interaction score could be calculated based on the number of interactions or acitivty like comment, video upload etc 
  const userInteractionData = [{'name': 'David', 'Role': 'Parent', 'interactions': 2},
  {'name': 'Eve', 'Role': 'Therapist', 'interactions': 6},
  {'name': 'Alice', 'Role': 'CareGiver', 'interactions': 9},
  {'name': 'Charlie', 'Role': 'Therapist', 'interactions': 3},
  {'name': 'Eve', 'Role': 'Therapist', 'interactions': 7},
  {'name': 'Alice', 'Role': 'CareGiver', 'interactions': 10},
  {'name': 'Bob', 'Role': 'Therapist', 'interactions': 5},
  {'name': 'Bob', 'Role': 'Parent', 'interactions': 3},
  {'name': 'Charlie', 'Role': 'CareGiver', 'interactions': 10},
  {'name': 'David', 'Role': 'Therapist', 'interactions': 7}]

  // List of recent  4 uploaded videos for learner and metadata like tags and title could be displayed
  const recent4Videos = [ 
    {
      "title": "Story",
      "tags" : ["positive", "learning"],
      "src": video1
    },
    {
      "title": "Cooking",
      "tags" : ["fun", "focus"],
      "src": video2
    },
    {
      "title": "Video Game",
      "tags" : ["social", "happy"],
      "src": video3
    },
    {
      "title": "Music",
      "tags" : ["calm", "relaxing"],
      "src": video4
    }
    ];

  // Picture of the day for learner 
  const pictureOfDay = picture;

  // Mock data for learning graph, this will be replaced with actual data
  const learningGraph = graph;

  // Mock data for progress score, this will be replaced with actual data after aggregation
  const progressScore = 87;

  return (
    <>
      <div>
        <InsightsNavbar />
        <Container fluid>
          <br></br>
          <Row className="top-row">
            <Col sm='6' >
              <DashboardCard title="Learning over time" image={learningGraph} />
            </Col>
            <Col sm='3'>
              <DashboardCard title="Progress Score" value={progressScore} />
            </Col>
            <Col sm='3'>
              <DashboardCard title="Access Users" list={userInteractionData} />
            </Col>
          </Row>
          <br></br>

          <Row className="middle-row">
            <h5> Recent Videos </h5>
            {recent4Videos.slice(0,4).map((video) => {
              return (
                <Col sm='3'>
                  <DashboardCard title={video.title} video={video.src} tags={video.tags} />
                </Col>
              )
            })
            }      
          </Row>
          <br></br>
          <Row className="bottom-row">
            <Col sm='2'>
              <DashboardCard title="Comment 1" comment="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. At tellus at urna condimentum mattis pellentesque id nibh." author="Jack" />
            </Col>
            <Col sm='2'>
              <DashboardCard title="Comment 2" comment="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua." author="Bob" />
            </Col>
            <Col sm='2'>
              <DashboardCard title="Comment 3" comment="Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Vel turpis nunc eget lorem dolor sed. Vivamus arcu felis bibendum ut tristique et egestas quis." author="Cam" />
            </Col>
            <Col sm='3'>
              <DashboardCard title="Picture of Day" image={pictureOfDay} />
            </Col>
            <Col sm='3'>
              <DashboardCard title="WordCount" words={sentiments} />
            </Col>
          </Row>
        </Container>
      </div>
    </>
  )
}

export default Dashboard;