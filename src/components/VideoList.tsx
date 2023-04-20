import React from 'react';
import { ListGroup } from 'react-bootstrap';


const VideoList = ({ videos, selectedVideo, onVideoSelect })  =>{

  const imgStyle = {
    width: '50px',
    maxHeight: '50px',
    float: 'left',
  
};
  return (
    <ListGroup style={{ maxHeight: '500px', overflowY: 'scroll' }}>
      {videos.map((video) => (
        <ListGroup.Item
          key={video.id.videoId}
          active={video === selectedVideo}
          onClick={() => onVideoSelect(video)}
        >
          <img src={video.snippet.thumbnails.medium.url} style={imgStyle} alt={video.snippet.title} />
          <div onClick={() => onVideoSelect(video)} >{video.snippet.title}</div>
        </ListGroup.Item>
      ))}
    </ListGroup>
  );
}

export default VideoList;