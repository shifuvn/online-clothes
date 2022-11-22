
import React, { useState } from "react"
import "bootstrap/dist/css/bootstrap.min.css"
import './LogSign.css';
// export default function (props) {
// export default Login = (props) => {
//     let [LoginSignUpMode, setLoginSignUpMode] = useState("signin")

//     const changeLoginSignUpMode = () => {
//         setLoginSignUpMode(LoginSignUpMode === "signin" ? "signup" : "signin")
//     }

//     if (LoginSignUpMode === "signin") {
//         return (
//             <div className="LoginSignUp-form-container">
//                 <form className="LoginSignUp-form">
//                     <div className="LoginSignUp-form-content">
//                         <h3 className="LoginSignUp-form-title">Sign In</h3>
//                         <div className="text-center">
//                             Not registered yet?{" "}
//                             <span className="link-primary" onClick={changeLoginSignUpMode}>
//                                 Sign Up
//                             </span>
//                         </div>
//                         <div className="form-group mt-3">
//                             <label>Email address</label>
//                             <input
//                                 type="email"
//                                 className="form-control mt-1"
//                                 placeholder="Enter email"
//                             />
//                         </div>
//                         <div className="form-group mt-3">
//                             <label>Password</label>
//                             <input
//                                 type="password"
//                                 className="form-control mt-1"
//                                 placeholder="Enter password"
//                             />
//                         </div>
//                         <div className="d-grid gap-2 mt-3">
//                             <button type="submit" className="btn btn-primary">
//                                 Submit
//                             </button>
//                         </div>
//                         <p className="text-center mt-2">
//                             Forgot <a href="#">password?</a>
//                         </p>
//                     </div>
//                 </form>
//             </div>
//         )
//     }

//     return (
//         <div className="LoginSignUp-form-container">
//             <form className="LoginSignUp-form">
//                 <div className="LoginSignUp-form-content">
//                     <h3 className="LoginSignUp-form-title">Sign In</h3>
//                     <div className="text-center">
//                         Already registered?{" "}
//                         <span className="link-primary" onClick={changeLoginSignUpMode}>
//                             Sign In
//                         </span>
//                     </div>
//                     <div className="form-group mt-3">
//                         <label>Full Name</label>
//                         <input
//                             type="email"
//                             className="form-control mt-1"
//                             placeholder="e.g Jane Doe"
//                         />
//                     </div>
//                     <div className="form-group mt-3">
//                         <label>Email address</label>
//                         <input
//                             type="email"
//                             className="form-control mt-1"
//                             placeholder="Email Address"
//                         />
//                     </div>
//                     <div className="form-group mt-3">
//                         <label>Password</label>
//                         <input
//                             type="password"
//                             className="form-control mt-1"
//                             placeholder="Password"
//                         />
//                     </div>
//                     <div className="d-grid gap-2 mt-3">
//                         <button type="submit" className="btn btn-primary">
//                             Submit
//                         </button>
//                     </div>
//                     <p className="text-center mt-2">
//                         Forgot <a href="#">password?</a>
//                     </p>
//                 </div>
//             </form>
//         </div>
//     )
// }


const LogSign = () => {
    let [LoginSignUpMode, setLoginSignUpMode] = useState("signin")

    const changeLoginSignUpMode = () => {
        setLoginSignUpMode(LoginSignUpMode === "signin" ? "signup" : "signin")
    }

    if (LoginSignUpMode === "signin") {
        return (
            <div className="LoginSignUp-form-container">
                <form className="LoginSignUp-form">
                    <div className="LoginSignUp-form-content">
                        <h3 className="LoginSignUp-form-title">Sign In</h3>
                        <div className="text-center">
                            Not registered yet?{" "}
                            <span className="link-primary" onClick={changeLoginSignUpMode}>
                                Sign Up
                            </span>
                        </div>
                        <div className="form-group mt-3">
                            <label>Email address</label>
                            <input
                                type="email"
                                className="form-control mt-1"
                                placeholder="Enter email"
                            />
                        </div>
                        <div className="form-group mt-3">
                            <label>Password</label>
                            <input
                                type="password"
                                className="form-control mt-1"
                                placeholder="Enter password"
                            />
                        </div>
                        <div className="d-grid gap-2 mt-3">
                            <button type="submit" className="btn btn-primary">
                                Submit
                            </button>
                        </div>
                        <p className="text-center mt-2">
                            Forgot <a href="#">password?</a>
                        </p>
                    </div>
                </form>
            </div>
        )
    }

    return (
        <div className="LoginSignUp-form-container">
            <form className="LoginSignUp-form">
                <div className="LoginSignUp-form-content">
                    <h3 className="LoginSignUp-form-title">Sign In</h3>
                    <div className="text-center">
                        Already registered?{" "}
                        <span className="link-primary" onClick={changeLoginSignUpMode}>
                            Sign In
                        </span>
                    </div>
                    <div className="form-group mt-3">
                        <label>Full Name</label>
                        <input
                            type="email"
                            className="form-control mt-1"
                            placeholder="e.g Jane Doe"
                        />
                    </div>
                    <div className="form-group mt-3">
                        <label>Email address</label>
                        <input
                            type="email"
                            className="form-control mt-1"
                            placeholder="Email Address"
                        />
                    </div>
                    <div className="form-group mt-3">
                        <label>Password</label>
                        <input
                            type="password"
                            className="form-control mt-1"
                            placeholder="Password"
                        />
                    </div>
                    <div className="d-grid gap-2 mt-3">
                        <button type="submit" className="btn btn-primary">
                            Submit
                        </button>
                    </div>
                    <p className="text-center mt-2">
                        Forgot <a href="#">password?</a>
                    </p>
                </div>
            </form>
        </div>
    )
}

export default LogSign