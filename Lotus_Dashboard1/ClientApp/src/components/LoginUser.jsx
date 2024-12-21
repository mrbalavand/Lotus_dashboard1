import React, { useState, useEffect } from 'react';
import "../../src/background.css";
import axios from "axios";
import * as yup from "yup";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUser, faMailBulk, faKey, faMobile } from '@fortawesome/free-solid-svg-icons';

import { useNavigate } from 'react-router-dom';
import { useContext } from 'react';
import AppContext from './AppContext';
import ReactLoading from 'react-loading';
import { faSignIn } from '../../../../node_modules/@fortawesome/free-solid-svg-icons/index';



export function LoginUser() {


    let [username, setusername] = useState("");
    let [password, setpassword] = useState("");

    let [error, seterror] = useState("");
    let [successregister, setsuccessregister] = useState("");
    let [loading, setloading] = useState("");

    const myContext = useContext(AppContext);
    console.log(myContext);


    useEffect(() => {

        did1()

    }, [])


    async function did1() {

        await setsuccessregister(`ورود به حساب کاربری`)
    }

    let history = useNavigate();

    function getusername(e) {

        setusername(e.target.value)
        console.log(username)

    }

    function getpassword(e) {

        setpassword(e.target.value)
        console.log(username)

    }



    function loginuser1() {

        let User1 = username;
        let Pass1 = password;

        setloading(`loading`)
        let formdata = { User: User1, Pass: Pass1 };

        console.log(formdata)

        const results = Validate(formdata);
        results.then((item) => {

            if (item.errors) {

                seterror(item.errors)
                console.log(error);
            }
            else {

                console.log("ok");
                seterror("");
                let Parameters = [{ Name: "User", Value: User1 }, { Name: "Pass", Value: Pass1 }];

                console.log(Parameters)

                axios.post(myContext.url + 'api/Login', Parameters).then((response) => {

                    console.log(response);

                    if (response.data.length >= 100) {
                        console.log("true")

                        setTimeout(() => { return "" }, 30000)
                        localStorage.setItem("lotus_dashboard", response.data)
                        setsuccessregister(`شما با موفقیت وارد شده اید`)
                        history("/")
                    }

                    else {
                        console.log("dublicate")
                        seterror([response.data]);
                        setloading(``)
                    }




                }).catch(() => {


                    setloading(``)



                }
                )

            }

            /* console.log(item.errors)*/




        })

    }

    const Validate = async (formdata) => {

        //let Name = document.querySelector("#Name");
        //let NationalCode = document.querySelector("#NationalCode");
        //let Email = document.querySelector("#Email");
        //let Mobile = document.querySelector("#Mobile");


        //setformdata({ Name: Name.value, NationalCode: NationalCode.value, Email: Email.value, Mobile: Mobile.value })

        let schema = yup.object().shape({

            User: yup.string("فرمت باید استرینگ باشد").required("فیلد نام کاربری الزامی است"),
            Pass: yup.string("فرمت باید استرینگ باشد").required("فیلد پسورد الزامی است").min(6, "پسورد باید حداقل 6 کاراکتر باشد"),

        })


        try {
            const result = await schema.validate(formdata, { abortEarly: false })

            return result

        } catch (err) {

            //setformdata({ Name: "", NationalCode: "", Email: "", Mobile: "" })

            return (err)


        }





    }


    useEffect(() => {

        document.addEventListener("keypress", (event) => {
            if (event.key === "Enter") {
                document.getElementById("btnlogin").click();
            }
        });

    }, []);

    return (
        <div className="row back shadow ">
            <div className="container ">
                <div className="col-lg-6 col-md-6 col-xs-12 text-white p-5 m-5">

                    <div className="offset-8 text-white col-6 justify-content-center" style={{ textAlign: "center" }}>
                        <div className="col-12 d-flex justify-content-center">
                            <img style={{ width: "30%", height: "30%" }} src="./logo.png" />
                        </div>
                        <div className="text-white mt-2" style={{ textAlign: "center",fontSize:"18px" }}>
                            {error.length !== 0 ? (error.map((item, i) => <p>{error[i]}</p>)) : (<p style={{ textAlign: "center" }}><div className="d-flex justify-content-center"><FontAwesomeIcon icon={faSignIn} size="lg" className="ps-1" color="rgb(255,181,0)" />&nbsp;&nbsp;{successregister}</div></p>)}
                            {loading.length !== 0 ? (<div className="d-flex justify-content-center"><ReactLoading type="spinningBubbles" color="white" height={"50px"} width={"50px"} color="rgb(254,254,254)" /></div>) : ("")}
                            {console.log(error)}

                        </div>
                    </div>


                    <div className="offset-8 col-6 input-group w-50 mb-3 pt-3 shadow justify-content-center">
                        <div className="input-group-prepend">
                            <div className="input-group-text border border-dark" id="basic-addon1" style={{ backgroundColor: "rgb(255,181,0)", height: "50px", borderColor: "rgb(100,100,100)", borderRadius: "10px" }}><FontAwesomeIcon icon={faUser} size="lg" className="ps-1" /></div>
                        </div>
                        <input type="text" className="form-control border border-dark w-50 shadow" placeholder="نام کاربری" onChange={getusername} value={username} style={{ height: "50px", borderRadius: "10px", fontSize: "14px" }} />
                    </div>



                    <div className="offset-8 col-6 w-50 input-group mb-3 shadow">
                        <div className="input-group-prepend border border-dark">
                            <div className="input-group-text border border-dark" id="basic-addon2" style={{ backgroundColor: "rgb(255,181,0)", height: "50px", borderColor: "rgb(100,100,100)", borderRadius: "10px" }}><FontAwesomeIcon icon={faKey} size="lg" className="ps-1" /></div>
                        </div>
                        <input type="password" className="form-control border border-dark w-50 shadow" placeholder="پسورد" onChange={getpassword} value={password} style={{ height: "50px", borderRadius: "10px", fontSize: "14px" }} />
                    </div>




                    <div className="offset-8 col-6 w-50 input-group mb-3">
                        <div className="input-group-prepend">

                        </div>
                        <button type="submit" className="form-control border border-dark shadow" style={{ height: "50px", backgroundColor: "rgb(255,181,0)", borderRadius: "10px" }} id="btnlogin" onClick={(e) => { loginuser1(e) }}>ورود به سامانه</button>
                    </div>

                </div>


            </div>

        </div>

    )


}