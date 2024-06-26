import { Link, useNavigate } from "react-router-dom";
import { OnBecomeDriverSubmit } from "../../services/UserService";
import { useForm } from "../../utils/hooks/useForm";
import "./BecomeDriver.css";
import { useState } from "react";

import backgroundImg from '../../utils/images/white-background-with-blue-geometric-and-white-line-pattern-free-vector.jpg'

export const BecomeDriver = () => {
  const [frontFileName, setFrontFileName] = useState([]);
  const [backFileName, setBackFileName] = useState([]);
  const navigate = useNavigate();

  const onChangeFrontFile = (e) => {
    const path = e.target.value.split("\\");
    const name = path[path.length - 1];

    setFrontFileName(name);
  };

  const onChangeBacktFile = (e) => {
    const path = e.target.value.split("\\");
    const name = path[path.length - 1];

    setBackFileName(name);
  };

  // const { values, changeHandler, onSubmit } = useForm({}, OnBecomeDriverSubmit);

  const onSubmit = async () => {
    await OnBecomeDriverSubmit();
    alert("🎉 Congratulations! 🎉\n\nYou are now a Driver! 🚗\n\nTo update your status, please re-login to your account.\n\n🔄 Re-login Required");
  }

  return (
    <div className="becomedriver-main">
        <img className="demo-bg" src={backgroundImg} />
        {localStorage.role == "driver" ? (
          <div className="you-are-driver">
            <h3>You are allready a Driver</h3>
            <div className="you-are-driver-links">
              <Link to={"/createPost"}>Add Post</Link>
              <Link to={"/createVehicle"}>Add Vehicle</Link>
            </div>
          </div>
        ) : (
          <div className="becomedriver-content">
            <h2>Become Driver</h2>
            <form className="becomedriver-form" onSubmit={onSubmit}>
              <div className="license-upload">
                <label>
                  Driver license Front
                  <input type="file" onChange={onChangeFrontFile} hidden />
                </label>
                <span>{frontFileName}</span>
              </div>
              <div className="license-upload">
                <label>
                  Driver license Back
                  <input type="file" onChange={onChangeBacktFile} hidden />
                </label>
                <span>{backFileName}</span>
              </div>
              <button>Submit</button>
            </form>
          </div>
        )}
    </div>
  );
};
