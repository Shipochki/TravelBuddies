import { useEffect, useState } from 'react';
import './App.css';
import { Route, Router, Routes } from 'react-router-dom';
import { GetAllCities } from './services/CityService';
import { Home } from './components/Home/Home';
import { Login } from './components/Login/Login';

function App() {
    const [cities, setCities] = useState([]);

    useEffect(() => {
        const cities = GetAllCities();
        setCities(cities);
    }, []);

    return (
        <div className='box'>
            <Routes>
                <Route path='/' element={<Home/>}/>
                <Route path='/login' element={<Login/>}/>
            </Routes>
        </div>
    )
}

export default App;