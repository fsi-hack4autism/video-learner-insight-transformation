import React from 'react';
import {
    Container,
    Row,
    Col,
    Card,
    Form,
    Button,
    InputGroup,
    FormControl,
} from 'react-bootstrap';
import '../styles/LoginSignup.css';
import MicrosoftLoginImage from '../assets/images/ms-symbollockup_signin_dark.png';

const LoginSignup = (props: any) => {

    return (
        <Container fluid >
            <Row className='d-flex justify-content-center align-items-center h-100' >
                <Col col='12'>
                    <Card className='my-5 mx-auto card-background' style={{ borderRadius: '1rem', maxWidth: '500px' }}>
                        <Card.Body className='p-5 w-100 d-flex flex-column'>
                            <h2 className="fw-bold mb-2 text-center label">Sign in</h2>
                            <p className="mb-3 label text-center ">Please enter your login and password!</p>

                            <Form.Group className='mb-4 w-100'>
                                <Form.Label className='label'>Email address</Form.Label>
                                <InputGroup >
                                    <FormControl type='email' size='lg' className='input' />
                                </InputGroup>
                            </Form.Group>

                            <Form.Group className='mb-4 w-100'>
                                <Form.Label className='label' >Password</Form.Label>
                                <InputGroup>
                                    <FormControl type='password' size='lg' className='input' />
                                </InputGroup>
                            </Form.Group>

                            <Form.Group className='mb-4'>
                                <Form.Check id='flexCheckDefault' label='Remember password' className='label' />
                            </Form.Group>

                            <Button size='lg' className='mb-3 login' >
                                Login
                            </Button>

                            <hr className="my-4 label" />
                            <Button size='lg' className='mb-3' style={{backgroundColor:`var(--blue)`}} onClick={(e) => {
      e.preventDefault();
      window.location.href='/dashboard';
      }}>Login With Microsoft</Button>
                        </Card.Body>
                    </Card>
                </Col>
            </Row>

        </Container>
    );
}

export default LoginSignup;
