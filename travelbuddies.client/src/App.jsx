import { useEffect, useState } from 'react';
import './App.css';
import { Route, Routes, useNavigate } from 'react-router-dom';
import { Home } from './pages/Home/Home';
import { Login } from './pages/Login/Login';
import { Regiser } from './pages/Register/Register';
import { Header } from './components/Header/Header';
import { Logout } from './pages/Logout/Logout'
import { BecomeDriver } from './pages/BecomeDriver/BecomeDriver'
import { Catalog } from './pages/Catalog/Catalog';
import { GlobalContext } from './utils/contexts/GlobalContext';
import { Search } from './pages/Search/Search'
import { CreatePost } from './pages/CreatePost/CreatePost';
import { About } from './pages/About/About';
import { Footer } from './components/Footer/Footer';
import { Groups } from './components/Groups/Groups';
import { Menu } from './components/Menu/Menu';
import { Group } from './pages/Group/Group';
import { Profile } from './pages/Profile/Profile';
import { Reviews } from './pages/Reviews/Reviews';
import { CreateVehicle } from './pages/CreateVehicle/CreateVehicle';
import { MyPosts } from './pages/MyPosts/MyPosts';
import { EditVehicle } from './pages/EditVehicle/EditVehicle';
import { MyVehicle } from './pages/MyVehicle/MyVehicle';
import { NotFound } from './pages/NotFound/NotFound';
import { ThemeProvider } from '@emotion/react';
import { createTheme } from '@mui/material';
import { Forbidden } from './pages/Forbidden/Forbidden';
import { BadRequest } from './pages/BadRequest/BadRequest';
import { GetAllGroupByUserId, GetGroupById } from './services/GroupService';
import { GetUserById } from './services/UserService';
import { EditPost } from './pages/EditPost/EditPost';
import { EditProfile } from './pages/EditProfile/EditProfile';

function App() {
    const navigate = useNavigate();
    const [group, setGroup] = useState({});
    const [groups, setGroups] = useState([]);
    const [user, setUser] = useState({});
    const [loading, setLoading] = useState(false);

    const OnSetGroup = async (id) => {
        const data = await GetGroupById(id);
        setGroup(data);
        navigate(`/group/${id}`);
    };

    const OnSetGroups = async () => {
        const data = await GetAllGroupByUserId();
        setGroups(data);
    };

    const OnSetUser = async (id) => {
        const data = await GetUserById(id);
        setUser(data);
        // navigate(`/profile/${id}`);
    }

    useEffect(() => {
        // Function to check token expiration
        const checkTokenExpiration = () => {
          const tokenExpiration = localStorage.exp;
          if (tokenExpiration) {
            const currentTime = Math.floor(Date.now() / 1000); // Current time in seconds
            if (currentTime >= tokenExpiration * 1000) {
              // Token is expired
              localStorage.clear();
              console.log('Access token has expired and has been removed.');
              navigate('/');
            }
          }
        };
    
        // Check token expiration on component mount
        checkTokenExpiration();
    
        // Set an interval to check token expiration periodically
        const intervalId = setInterval(checkTokenExpiration, 60000); // Check every 60 seconds
    
        // Clean up the interval on component unmount
        return () => clearInterval(intervalId);
      }, []);

    const theme = createTheme({
        palette:{
            primary:{
                main: '#2979ff',
                light: 'white',
            },
            secondary:{
                main: '#F5F6F7'
            }
        },
        typography: {
            h2: {
              fontSize: '1rem',
              '@media (min-width:600px)': {
                fontSize: '1.5rem',
              },
              '@media (min-width:960px)': {
                fontSize: '2rem',
              },
            },
          },
      });

    const globalContext = {
        OnSetGroup,
        OnSetGroups,
        OnSetUser
    }

    return (
        <GlobalContext.Provider value={globalContext}>
            <ThemeProvider theme={theme}>
                <div className='box'>
                    <Header/>
                    {localStorage.accessToken && (
                        <>
                            <Groups groups={groups}/>
                            <Menu />
                        </>
                    )}
                    <Routes>
                        {localStorage.accessToken ? (
                            <>
                                <Route path='/' element={<Search/>}/>
                                <Route path='/search' element={<Search/>}/>
                                <Route path='/logout' element={<Logout/>}/>
                                <Route path='/becomeDriver' element={<BecomeDriver/>}/>
                                <Route path='/createPost' element={<CreatePost/>}/>
                                <Route path='/catalog' element={<Catalog/>}/>
                                <Route path='/group/:id' element={<Group group={group}/>}/>
                                <Route path='/profile/:id' element={<Profile user={user}/>}/>
                                <Route path='/editProfile' element={<EditProfile />}/>
                                <Route path='/reviews/:id' element={<Reviews/>}/>
                                <Route path='/createVehicle' element={<CreateVehicle/>}/>
                                <Route path='/editVehicle' element={<EditVehicle/>}/>
                                <Route path='/myVehicle' element={<MyVehicle/>}/>
                                <Route path='/myPosts' element={<MyPosts/>}/>
                                <Route path='/forbidden' element={<Forbidden/>}/>
                                <Route path='/badRequest' element={<BadRequest/>}/>
                                <Route path='/editPost/:id' element={<EditPost/>}/>
                            </>
                        ) : (
                            <>
                                <Route path='/' element={<Home/>}/>
                                <Route path='/login' element={<Login/>}/>
                                <Route path='/register' element={<Regiser/>}/>
                                <Route path='/about' element={<About/>}/>
                            </>
                        )} 
                    <Route path='*' element={<NotFound/>}/>
                </Routes>
                <Footer/>
                </div>
            </ThemeProvider>
        </GlobalContext.Provider>
    )
}

export default App;