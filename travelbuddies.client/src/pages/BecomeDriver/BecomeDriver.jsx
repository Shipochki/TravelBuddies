import { Link } from 'react-router-dom'
import { OnBecomeDriverSubmit } from '../../services/UserService'
import { useForm } from '../../utils/hooks/useForm'
import './BecomeDriver.css'

export const BecomeDriver = () => {
    const { values, changeHandler, onSubmit } = useForm({   
    }, OnBecomeDriverSubmit)

    return (
        <div className="becomedriver-main">
            {localStorage.role == 'driver' ? (
                <div className='you-are-driver'>
                    <h3>You are allready a Driver</h3>
                    <div className='you-are-driver-links'>
                        <Link to={'/createPost'}>Add Post</Link>
                        <Link to={'/createVehicle'}>Add Vehicle</Link>
                    </div>
                </div>
            ) : (
                <div className="becomedriver-content">
                    <form className='becomedriver-form' onSubmit={onSubmit}>
                        <div className='license-upload'>
                            <label>Driver license front</label>
                            <input 
                            type="file"
                            />
                        </div>
                        <div className='license-upload'>
                            <label>Driver license Back</label>
                            <input 
                            type="file"
                            />
                        </div>
                        <button>Submit</button>
                    </form>
                </div>
            )}
        </div>
    )
}