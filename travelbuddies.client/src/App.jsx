import { useEffect, useState } from 'react';
import './App.css';
import { Route, Router, Routes } from 'react-router-dom';
import { GetAllCities } from './services/CityService';
import { Home } from './pages/Home/Home';
import { Login } from './pages/Login/Login';
import { Regiser } from './pages/Register/Register';
import { Header } from './components/Header/Header';
import { Logout } from './pages/Logout/Logout'
import { BecomeDriver } from './pages/BecomeDriver/BecomeDriver'
import { Catalog } from './pages/Catalog/Catalog';
import { GlobalContext } from './utils/contexts/GlobalContext';
import { Search } from './pages/Search/Search'

function App() {
    const [cities, setCities] = useState([]);
    const [posts, setPosts] = useState([]);

    //useEffect(() => {
    //    const result = GetAllCities();
    //    setCities(result);
    //}, []);

    useEffect(() => {
        const GetAllCities = async () => {
          try {
            const response = await fetch('https://localhost:7005/api/City/GetCities', {
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
        }; GetAllCities()
        }, []);
    
    const globalContext = {
        setPosts,
    }

    return (
        <GlobalContext.Provider value={globalContext}>
            <div className='box'>
                <Header/>

                <Routes>
                    {localStorage.accessToken ? (
                        <>
                            <Route path='/' element={<Search cities={cities}/>}/>
                            <Route path='/logout' element={<Logout/>}/>
                            {localStorage.role == 'client' ? (
                                <Route path={'/becomeDriver'} element={<BecomeDriver/>}/>
                            ): ''}
                            <Route path='/Catalog' element={<Catalog/>}/>
                        </>
                    ) : (
                        <>
                            <Route path='/' element={<Home/>}/>
                            <Route path='/login' element={<Login/>}/>
                            <Route path='/register' element={<Regiser/>}/>
                        </>
                    )}
                </Routes>
            </div>
        </GlobalContext.Provider>
    )
}

export default App;