import { Link } from 'react-router-dom'
import './Home.css'

export const Home = () => {
    return(
        <div className='home-main'>
            <div className='home-content'>
                <Link to={'/login'}>Login</Link>
            </div>
        </div>
    )
}