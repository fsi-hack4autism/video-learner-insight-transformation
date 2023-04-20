import React from 'react';
import { Card } from 'react-bootstrap';
import '../styles/Dashboard.css';
import ReactWordcloud from 'react-wordcloud';
import Table from 'react-bootstrap/Table';


interface Word {
    [key: string]: any;
    text: string;
    value: number;
}

interface BaseCardProps {
    color?: string;
    children?: React.ReactNode;
    title?: string;
    height?: string;
}

interface GraphCardProps extends BaseCardProps {
    data: any[];
}

interface TextCardProps extends BaseCardProps {
    value: string;
}

interface VideoCardProps extends BaseCardProps {
    video: string;
    poster?: string;
    tags: string[];
    callback?: (e: any) => void;
}

interface WorldCloudCardProps extends BaseCardProps {
    words: Word[];
}

interface CommentCardProps extends BaseCardProps {
    comment: string;
    author: string;
    height?: string;
}
interface ImageCardProps extends BaseCardProps {
    image: string;
}

interface dataList {
    name: string;
    Role: string;
    interactions: number;
}



interface TableCardProps extends BaseCardProps {
    list: dataList[];
}


type CardProps =
    | GraphCardProps
    | TextCardProps
    | VideoCardProps
    | WorldCloudCardProps
    | CommentCardProps
    | ImageCardProps
    | TableCardProps;

const BaseCard = ({ color = 'blue', children, height = "inhert" }: BaseCardProps) => {
    const cardStyle = {
        backgroundColor: `var(--${color})`,
        minHeight: '20vh',
        height: `${height} !important`,
    };

    return (
        <Card className="border-0 dash-card">
            <Card.Body style={cardStyle}>
                {children}
            </Card.Body>
        </Card>
    );
};

const GraphCard = ({ title, data, color = 'primary', children }: GraphCardProps) => {
    return (
        <BaseCard color={color}>
            <Card.Title>{title}</Card.Title>
            <Card.Text>{JSON.stringify(data)}</Card.Text>
            {children}
        </BaseCard>
    );
};

const TextCard = ({ title, value, color = 'primary', children }: TextCardProps) => {
    
    let cardStyle = {
        fontSize: '20px'
    };
    if (title === "Story" || title === "Music" || title === "Cooking" || title === "Video Game"){
        cardStyle = {
            fontSize: '20px'
        };
    }
    return (
        <BaseCard color={color}>
            <Card.Title>{title}</Card.Title>
            <Card.Text style={cardStyle}>{value}</Card.Text>
            {children}
        </BaseCard>
    );
};

const ImageCard = ({ title, image, color = 'primary', children }: ImageCardProps) => {
    const imgStyle = {
        width: '-webkit-fill-available',        
    };

    return (
        <BaseCard color={color}>
            <Card.Title>{title}</Card.Title>
            <img src={image} alt={title} style={imgStyle} />
        </BaseCard>
    );
};

const VideoCard = ({ title, video, poster, tags, color = 'white', children, callback }: VideoCardProps) => {
    const videoStyle = {
        width: '-webkit-fill-available'
    };
    return (
        <BaseCard color={color}>
            <Card.Body className='p-0'>
                <video controls poster={poster} style={videoStyle}>
                    <source src={video} type="video/mp4" />
                    Your browser does not support the video tag.
                </video>
               <Card.Title 
                    key={title}
                        onClick={callback}
                        >
                        {title}
                </Card.Title>
                {children}
                {tags.map((item, index) => (
                    <><span style={{ backgroundColor: '#D3D3D3', fontWeight: 'bold' }} key={index}>#{item} </span><span> &nbsp; </span></>
                ))}
            </Card.Body>
        </BaseCard>
    );
};

const WorldCloudCard = ({ words, color = 'white', children }: WorldCloudCardProps) => {
    return (
        <BaseCard color={color}>
            <Card.Title>Sentiment Word Cloud</Card.Title>
            <ReactWordcloud words={words} />
        </BaseCard>
    );
};

const CommentCard = ({ comment, author, color = 'yellow', children, height }: CommentCardProps) => {
    return (
        <BaseCard color={color} height={height}>
            <Card.Text>{comment}</Card.Text>
            <footer className="blockquote-footer">{author}</footer>
            {children}
        </BaseCard>
    );
};

const TableCard = ({ list, color = 'white', children }: TableCardProps) => {
    return (
        <BaseCard color={color}>
            <Card.Title>Top 10 Interactions Score</Card.Title>
            <Table striped bordered>
  <thead>
    <tr>
      <th>User</th>
      <th>Role</th>
      <th>Interactions</th>
    </tr>
  </thead>
  <tbody>
    {list.map((item, index) => (
        <tr key={index}>
            {Object.keys(item).map((key, index) => (
                <td key={index}>{item[key]}</td>
            ))}
        </tr>
    ))}
  </tbody>
</Table>
        </BaseCard>
    );
};



const DashboardCard = (props: CardProps) => {
    if ('data' in props) {
        return <GraphCard {...props as GraphCardProps} />;
    } else if ('title' in props && 'value' in props) {
        return <TextCard {...props as TextCardProps} />;
    } else if ('video' in props) {
        return <VideoCard {...props as VideoCardProps} />;
    } else if ('words' in props) {
        return <WorldCloudCard {...props as WorldCloudCardProps} />;
    } else if ('comment' in props && 'author' in props) {
        return <CommentCard {...props as CommentCardProps} />;
    } else if ('image' in props) {
        return <ImageCard {...props as ImageCardProps} />;
    } else if ('list' in props) {
        return <TableCard {...props as TableCardProps} />;
    } else {
        return null;
    }
};

export default DashboardCard;
