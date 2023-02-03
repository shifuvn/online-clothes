import styled from "styled-components";
import InstagramIcon from '@mui/icons-material/Instagram';
import FacebookIcon from '@mui/icons-material/Facebook';
//import InstagramIcon from '@mui/icons-material/Instagram';
import PhoneIcon from '@mui/icons-material/Phone';
import RoomIcon from '@mui/icons-material/Room';
import MailOutlineIcon from '@mui/icons-material/MailOutline';
const Container = styled.div`
    display: flex;
    
  `;

const Left = styled.div`
    flex: 1;
    display: flex;
    flex-direction: column;
    padding: 20px;
  `;

const Logo = styled.h1``;

const Desc = styled.p`
    margin: 20px 0px;
  `;

const SocialContainer = styled.div`
    display: flex;
  `;

const SocialIcon = styled.div`
    width: 40px;
    height: 40px;
    border-radius: 50%;
    color: white;
    
    display: flex;
    align-items: center;
    justify-content: center;
    margin-right: 20px;
  `;

const Center = styled.div`
    flex: 1;
    padding: 20px;
 
  `;

const Title = styled.h3`
    margin-bottom: 30px;
  `;

const List = styled.ul`
    margin: 0;
    padding: 0;
    list-style: none;
    display: flex;
    flex-wrap: wrap;
  `;

const ListItem = styled.li`
    width: 50%;
    margin-bottom: 10px;
  `;

const Right = styled.div`
    flex: 1;
    padding: 20px;
    
  
  `;

const ContactItem = styled.div`
    margin-bottom: 20px;
    display: flex;
    align-items: center;
  `;

const Payment = styled.img`
      width: 50%;
  `;

const Footer = () => {
    return (
        <Container>
            <Left>
                <Logo>LAMA.</Logo>
                <Desc>
                    Luôn luôn lắng nghe sẵn sàng chia sẻ dù bán hay không
                </Desc>
                <SocialContainer>
                    <SocialIcon color="3B5999">
                        <FacebookIcon />
                    </SocialIcon>
                    <SocialIcon color="E4405F">
                        <InstagramIcon />
                    </SocialIcon>


                </SocialContainer>
            </Left>
            <Center>
                <Title>Useful Links</Title>
                <List>
                    <ListItem>Home</ListItem>
                    <ListItem>Cart</ListItem>
                    <ListItem>Man Fashion</ListItem>
                    <ListItem>Woman Fashion</ListItem>
                    <ListItem>Accessories</ListItem>
                    <ListItem>My Account</ListItem>
                    <ListItem>Order Tracking</ListItem>
                    <ListItem>Wishlist</ListItem>
                    <ListItem>Wishlist</ListItem>
                    <ListItem>Terms</ListItem>
                </List>
            </Center>
            <Right>
                <Title>Contact</Title>
                <ContactItem>
                    <RoomIcon style={{ marginRight: "10px" }} /> kp 6, Linh Trung, Thu Duc city, Ho Chi Minh city
                </ContactItem>
                <ContactItem>
                    <PhoneIcon style={{ marginRight: "10px" }} /> +84 9824734
                </ContactItem>
                <ContactItem>
                    <MailOutlineIcon style={{ marginRight: "10px" }} /> abc@gmail.com
                </ContactItem>
                <Payment src="https://i.ibb.co/Qfvn4z6/payment.png" />
            </Right>
        </Container>
    );
};

export default Footer;