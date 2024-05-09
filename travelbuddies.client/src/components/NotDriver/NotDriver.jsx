import { Link } from 'react-router-dom'
import './NotDriver.css'

export const NotDriver = () => {

    return(
        <div className="not-driver">
            <h3>You are not Driver</h3>
            <Link to={'/becomeDriver'}>Become Driver</Link>
        </div>
    )
}