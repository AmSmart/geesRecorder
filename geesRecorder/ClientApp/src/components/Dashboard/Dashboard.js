import React, { Component } from 'react';
import { Container } from 'reactstrap';

import {
    Card, CardImg, CardText, CardBody,
    CardTitle, CardSubtitle, Button,
    Row, Col
  } from 'reactstrap';



 export   const Dashboard = (props) => {
        return (
          <div>
              <Container>
                  <Row>
                  <Row>
<Col> 
 <Card>
              <CardImg top width="auto" src="/assets/318x180.svg" alt="Card image cap" />
              <CardBody>
                <CardTitle>Card title</CardTitle>
                <CardSubtitle>Card subtitle</CardSubtitle>
                <CardText>Some quick example text to build on the card title and make up the bulk of the card's content.</CardText>
                <Button>Button</Button>
              </CardBody>
            </Card>
            </Col>
<Col>
<Card>
              <CardImg top width="auto" src="/assets/318x180.svg" alt="Card image cap" />
              <CardBody>
                <CardTitle>Card title</CardTitle>
                <CardSubtitle>Card subtitle</CardSubtitle>
                <CardText>Some quick example text to build on the card title and make up the bulk of the card's content.</CardText>
                <Button>Button</Button>
              </CardBody>
            </Card>
</Col>
                  </Row>
                  <Row>
                      <Col>
                      <Card>
              <CardImg top width="auto" src="/assets/318x180.svg" alt="Card image cap" />
              <CardBody>
                <CardTitle>Card title</CardTitle>
                <CardSubtitle>Card subtitle</CardSubtitle>
                <CardText>Some quick example text to build on the card title and make up the bulk of the card's content.</CardText>
                <Button>Button</Button>
              </CardBody>
            </Card>
                      </Col>
                      <Col>
                      <Card>
              <CardImg top width="auto" src="/assets/318x180.svg" alt="Card image cap" />
              <CardBody>
                <CardTitle>Card title</CardTitle>
                <CardSubtitle>Card subtitle</CardSubtitle>
                <CardText>Some quick example text to build on the card title and make up the bulk of the card's content.</CardText>
                <Button>Button</Button>
              </CardBody>
            </Card>
                      </Col>

                  </Row>
                  </Row>

              </Container>
          
          </div>
        );
      };













