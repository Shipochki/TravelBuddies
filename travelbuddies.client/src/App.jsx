import { useState } from 'react';
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

function App() {
    const navigate = useNavigate();
    const [posts, setPosts] = useState([]);
    const [group, setGroup] = useState([]);
    const [user, setUser] = useState([]);
    const [reviews, setReviews] = useState([]);
    const [vehicle, setVehicle] = useState([]);

    const OnSetPosts = (posts) => {
        setPosts(posts);

        navigate('/catalog');
    }

    const OnSetGroup = (group) => {
        setGroup(group);
        navigate('/group');
    }

    const OnSetUser = (user) => {
        setUser(user);
        navigate('/profile')
    }

    const OnSetReviews = (reviews) => {
        setReviews(reviews);
        navigate('/reviews')
    }

    const OnSetVehicle = (vehicle) => {
        setVehicle(vehicle);
        navigate('/myVehicle')
    }

    const OnSetPostsByOwner = (posts) => {
        setPosts(posts);
        navigate('/myPosts')
    }

    const globalContext = {
        OnSetPosts,
        OnSetGroup,
        OnSetUser,
        OnSetReviews,
        OnSetVehicle,
        OnSetPostsByOwner
    }

    return (
        <GlobalContext.Provider value={globalContext}>
            <div className='box'>
                <Header/>
                {localStorage.accessToken && (
                    <>
                        <Groups/>
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
                            <Route path='/group/:id' element={<Group/>}/>
                            <Route path='/profile/:id' element={<Profile/>}/>
                            <Route path='/reviews/:id' element={<Reviews/>}/>
                            <Route path='/createVehicle' element={<CreateVehicle/>}/>
                            <Route path='/editVehicle' element={<EditVehicle/>}/>
                            <Route path='/myVehicle' element={<MyVehicle vehicle={vehicle}/>}/>
                            <Route path='/myPosts' element={<MyPosts/>}/>
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
        </GlobalContext.Provider>
    )
}

export default App;