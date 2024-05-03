import { useEffect, useState } from 'react';
import './App.css';
import { Route, Router, Routes, useNavigate } from 'react-router-dom';
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

function App() {
    const navigate = useNavigate();
    const [cities, setCities] = useState([]);
    const [posts, setPosts] = useState([]);
    const [groups, setGroups] = useState([]);
    const [messages, setMessages] = useState([]);

    useEffect(() => {
        const GetAllCities = async () => {
          try {
            const response = await fetch('https://localhost:7005/api/city/getcities', {
                method: 'GET',
                mode: "cors",
                headers: {
                    'Authorization': `Bearer ${localStorage.accessToken}`,
                    'Content-Type': 'application/json'
                }});
            const cityNames = await response.json();
            setCities(cityNames);
          } catch (error) {
            console.error('Error fetching cities:', error);
          }
        }; GetAllCities();
        }, []);
    
    useEffect(() => {
        const GetAllGroupByUserId = async () => {
            try {
                const response = await fetch('https://localhost:7005/api/group/getusergroupsbyuserid', {
                  method: 'GET',
                  mode: "cors",
                  headers: {
                      'Authorization': `Bearer ${localStorage.accessToken}`,
                      'Content-Type': 'application/json'
                  },
                });

                const groups = await response.json()
                setGroups(groups);
                
              } catch (error) {
                console.error('Error fetching join group:', error);
              }}
              ; GetAllGroupByUserId()
    }, [])

    const OnSetPosts = (posts) => {
        console.log(posts);
        setPosts(posts);

        navigate('/catalog');
    }

    const OnSetMessages = (messages) => {
        setMessages(messages);

        navigate('/group');
    }

    const globalContext = {
        OnSetPosts,
        OnSetMessages
    }

    return (
        <GlobalContext.Provider value={globalContext}>
            <div className='box'>
                <Header/>
                {localStorage.accessToken && (
                    <Groups groups={groups}/>
                )}
                <Routes>
                    {localStorage.accessToken ? (
                        <>
                            <Route path='/' element={<Search cities={cities}/>}/>
                            <Route path='/search' element={<Search cities={cities}/>}/>
                            <Route path='/logout' element={<Logout/>}/>
                            {localStorage.role == 'client' ? (
                                <Route path={'/becomeDriver'} element={<BecomeDriver/>}/>
                            ): ''}
                            {localStorage.role == 'driver' ? (
                                <Route path={'/createPost'} element={<CreatePost cities={cities}/>}/>
                            ): ''}
                            <Route path='/catalog' element={<Catalog posts={posts}/>}/>
                        </>
                    ) : (
                        <>
                            <Route path='/' element={<Home/>}/>
                            <Route path='/login' element={<Login/>}/>
                            <Route path='/register' element={<Regiser/>}/>
                        </>
                    )} 
                    <Route path='/about' element={<About/>}/>
                </Routes>

                <Footer/>
            </div>
        </GlobalContext.Provider>
    )
}

export default App;