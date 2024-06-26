import { LazyLoadImage } from "react-lazy-load-image-component";
import "./Home.css"
import { Link } from "react-router-dom";
import 'react-lazy-load-image-component/src/effects/blur.css';
import thirdLayerImg1 from '../../utils/images/austin-distel-wD1LRb9OeEo-unsplash.jpg'
import thirdLayerImg2 from '../../utils/images/austin-kehmeier-lyiKExA4zQA-unsplash.jpg'
import thirdLayerImg3 from '../../utils/images/girl-with-red-hat-gL3-jTs_Q7g-unsplash.jpg'
import fourthLayerImg1 from '../../utils/images/felix-rostig-UmV2wr-Vbq8-unsplash.jpg'
import fourthLayerImg2 from '../../utils/images/jorge-saavedra-94qZsII4kN8-unsplash.jpg'
import fourthLayerImg3 from '../../utils/images/helena-lopes-PGnqT0rXWLs-unsplash.jpg'
import fourthLayerImg4 from '../../utils/images/scott-dukette-1HpQU1evGK8-unsplash.jpg'

export const Home = () => {
    return (
        <div className="home-page">
            <div className="first-layer">
                <div className="first-layer-content">
                    <p>Travel with us</p>
                    {localStorage.accessToken ? (
                        <Link to={'/search'}>Let's go</Link>
                    ): (
                        <Link to={'/login'}>Let's go</Link>
                    )}
                </div>
            </div>
            <div className="second-layer">
                <h2>This Is Our Story</h2>
                <p>In the bustling realm of modern life, where every moment seems 
                to tick faster than the last, the idea of shared traveling emerged 
                as a beacon of connection, weaving together the threads of 
                our individual narratives into a vibrant tapestry of shared experiences. 
                This is our story—a tale of exploration, camaraderie, and the boundless 
                horizons we discovered together through an application designed to unite 
                hearts and minds on the road less traveled.</p>
                <Link to={'/about'}>Read More</Link>
            </div>
            <div className="third-layer">
                <h2>What Makes Us Special</h2>
                <div className="third-layer-content">
                    <div className="third-layer-card">
                        <LazyLoadImage 
                        effect="blur"
                        src={thirdLayerImg1} alt="community" />
                        <h3>Community-Centric Approach</h3>
                        <p>Our application isn't just about facilitating 
                        travel plans; it's about cultivating a vibrant 
                        community of like-minded individuals who share 
                        a passion for exploration and connection.</p>
                    </div>
                    <div className="third-layer-card">
                        <LazyLoadImage
                        effect="blur"
                        src={thirdLayerImg2} alt="Emphasis" />
                        <h3>Emphasis on Safety and Inclusivity</h3>
                        <p>We prioritize the safety and well-being of our 
                        users above all else, implementing robust safety 
                        measures and fostering a culture of inclusivity 
                        where everyone feels respected, valued, and heard. 
                        By creating a space where people from all walks of 
                        life can come together without fear of discrimination 
                        or harm, we're building a stronger, more connected 
                        world one journey at a time.</p>
                    </div>
                    <div className="third-layer-card">
                        <LazyLoadImage
                        effect="blur"
                        src={thirdLayerImg3} alt="Focus " />
                        <h3>Focus on Authentic Experiences</h3>
                        <p>Through cutting-edge algorithms and user-friendly 
                        interfaces, we strive to make the process of finding 
                        travel companions as seamless and enjoyable as possible. 
                        Whether you're seeking someone with similar interests, 
                        travel preferences, or personality traits, our application 
                        uses advanced matching algorithms to connect you with the 
                        perfect travel buddy for your next adventure.</p>
                    </div>
                </div>
            </div>
            <div className="fourth-layer">
                <LazyLoadImage
                effect="blur"
                src={fourthLayerImg1} alt="people" />
                <LazyLoadImage 
                effect="blur"
                src={fourthLayerImg2} alt="two girls" />
                <LazyLoadImage
                effect="blur" 
                src={fourthLayerImg3} alt="friends" />
                <LazyLoadImage
                effect="blur" 
                src={fourthLayerImg4} alt="car" />
            </div>
            <div className="fifth-layer">
                <h3>“Not all those who wander are lost.”</h3>
                <p>J.R.R. Tolkien</p>
            </div>
        </div>
    )
}