import { LazyLoadImage } from 'react-lazy-load-image-component';
import './About.css';

export const About = () => {
    return(
        <div className="about-content">
            <h3>This Is Our Story</h3>
            <LazyLoadImage src={'https://sttravelbuddies001.blob.core.windows.net/web/jez-timms-_Ch_onWf38o-unsplash.jpg'} alt="spark" />
            <p className='topic'>The Spark of Inspiration</p>
            <p className='topic-content'>It all began with a spark—an ember of 
                inspiration ignited by our collective yearning 
                for adventure and human connection. In a world 
                where distances often grew wider even as technology 
                bridged gaps, we found ourselves longing for genuine 
                interactions and meaningful journeys. Thus, 
                the concept of an application for shared traveling 
                was born—a digital platform where fellow wanderers 
                could converge, sharing not just destinations but 
                dreams, aspirations, and the sheer joy of exploration.</p>
        </div>
    )
}