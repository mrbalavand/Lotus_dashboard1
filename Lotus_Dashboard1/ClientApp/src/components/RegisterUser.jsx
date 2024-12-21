import { useState,useEffect } from 'react';
import "../../src/background.css";
import axios from "axios";
import * as yup from "yup";
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faUser, faMailBulk, faKey, faMobile } from '@fortawesome/free-solid-svg-icons';
import AppContext from './AppContext';
import { useContext } from 'react';


export function RegisterUser() {

    let [fname, setfname] = useState("");
    let [username, setusername] = useState("");
    let [password, setpassword] = useState("");
    let [email, setemail] = useState("");
    let [mobile, setmobile] = useState("");
    let [error, seterror] = useState("");
    let [successregister, setsuccessregister] = useState("");
    const myContext = useContext(AppContext);

    useEffect(() => {

        do1()

    }, [])

    async function do1() {

        await setsuccessregister(`ثبت نام کاربر جدید`)
    }


    function getusername(e) {

        setusername(e.target.value)
        

    }

    function getpassword(e) {

        setpassword(e.target.value)
        

    }


    function getemail(e) {

        setemail(e.target.value)
        

    }

    function getmobile(e) {

        setmobile(e.target.value)
        

    }


    function getfname(e) {

        setfname(e.target.value)
        

    }


    function savedata() {

        let User1 = username;
        let Pass1 = password;
        let Email1 = email;
        let Mobile1 = mobile;
        let Fname1 = fname;

        let formdata = { User: User1, Pass: Pass1, Email: Email1, Mobile: Mobile1,Fname:Fname1 };

        console.log(formdata)

        const results = Validate(formdata);
        console.log(results)
        results.then((item) => {

            if (item.errors) {
                
                seterror(item.errors)
                console.log(error);
            }
            else {

                console.log("ok");
                seterror("");
                let Parameters = [{ Name: "User", Value: User1 }, { Name: "Email", Value: Email1 }, { Name: "Pass", Value: Pass1 }, { Name: "Mobile", Value: Mobile1 }, { Name: "Fname", Value: Fname1 }];

                console.log(Parameters)

                axios.post(myContext.url + 'api/Register', Parameters, {
                    headers: {
                        'Content-Type': 'application/json',
                    }
                    }).then((response) => {

                    console.log(response);

                    if (response.data===true) {
                        console.log("true")
                        setsuccessregister(`ثبت نام شما با موفقیت انجام شد. لطفا <a href="./loginuser">لاگین</a> نمایید`)

                    }
                    else if (response.data[0] === "DuplicateUserName"){
                        console.log("dublicate")
                        seterror(["نام کاربری تکراری است"]);
                    }
                    else if (response.data[0] !== true) {
                        console.log("dublicate")
                        seterror([response.data]);
                    }




                }).catch(() => {



                    console.log("error")


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

            Fname: yup.string("فرمت باید استرینگ باشد").required("فیلد نام الزامی است"),
            User: yup.string("فرمت باید استرینگ باشد").required("فیلد نام کاربری الزامی است"),
            Pass: yup.string("فرمت باید استرینگ باشد").required("فیلد پسورد الزامی است").min(6, "پسورد باید حداقل 6 کاراکتر باشد"),
            Email: yup.string("فرمت باید استرینگ باشد").required("فیلد ایمیل الزامی است").email("فرمت ایمیل اشتباه است"),
            Mobile: yup.string("فرمت باید عدد باشد").required("فیلد موبایل الزامی است").min(11, "موبایل باید 11 کاراکتر باشد").matches(/^((\\+[1-9]{1,4}[ \\-]*)|(\\([0-9]{2,3}\\)[ \\-]*)|([0-9]{2,4})[ \\-]*)*?[0-9]{3,4}?[ \\-]*[0-9]{3,4}?$/, 'شماره موبایل معتبر نمیباشد'),

        })


        try {
            const result = await schema.validate(formdata, { abortEarly: false })

            return result

        } catch (err) {

            //setformdata({ Name: "", NationalCode: "", Email: "", Mobile: "" })

            return (err)


        }





    }

    return (
        <div className="row back p-0 m-0 shadow">
            <div className="col-md-3 text-white parent" style={{ borderRadius: "20px", marginTop: "100px", marginBottom: "400px", marginRight: "200px", border: "groov", borderColor: "black",position:"absolute" }}>
              
                <div className="text-white col-12 shadow" style={{ textAlign: "center", marginTop: "80px" }}>
                    <img style={{ width: "140px", height: "70px" }} src="./logo.png"/>
                    <div className="text-white">
                        {error.length !== 0 ? (error.map((item, i) => <p>{error[i]}</p>)) : (<p>{(successregister)}</p>)}

                        {console.log(error)}

                    </div>
                </div>



                <div className="input-group mb-0 pt-3 shadow">
                    <div className="input-group-prepend">
                        <span className="input-group-text border border-dark" id="basic-addon1" style={{ backgroundColor: "rgb(255,181,0)", height: "50px", width: "50px", borderColor: "rgb(100,100,100)", borderRadius: "10px" }}><FontAwesomeIcon icon={faUser} size="lg" className="ps-1" /></span>
                    </div>
                    <input type="text" className="form-control border border-dark" placeholder="نام و نام خانوادگی" onChange={getfname} value={fname} style={{ height: "50px", width: "200px", borderRadius: "10px", fontSize: "14px" }} />
                </div>

                <div className="input-group mb-3 pt-3 shadow">
                    <div className="input-group-prepend">
                        <span className="input-group-text border border-dark" id="basic-addon1" style={{ backgroundColor: "rgb(255,181,0)", height: "50px", width: "50px", borderColor: "rgb(100,100,100)", borderRadius: "10px" }}><FontAwesomeIcon icon={faUser} size="lg" className="ps-1"/></span>
                    </div>
                    <input type="text" className="form-control border border-dark" placeholder="نام کاربری" onChange={getusername} value={username} style={{ height: "50px", width: "200px", borderRadius: "10px", fontSize: "14px" }}/>
                </div>


                <div className="input-group mb-3">
                    <div className="input-group-prepend">
                        <span className="input-group-text border border-dark" id="basic-addon1" style={{ backgroundColor: "rgb(255,181,0)", height: "50px", width: "50px", borderColor: "rgb(100,100,100)", borderRadius: "10px" }}><FontAwesomeIcon icon={faMailBulk} size="lg" className="ps-1" /></span>
                    </div>
                    <input type="text" className="form-control border border-dark" placeholder="ایمیل" onChange={getemail} value={email} style={{ height: "50px", width: "200px", borderRadius: "10px", fontSize: "14px"  }} />
                </div>


                <div className="input-group mb-3">
                    <div className="input-group-prepend border border-dark">
                        <span className="input-group-text" id="basic-addon1" style={{ backgroundColor: "rgb(255,181,0)", height: "50px", width: "50px", borderColor: "rgb(100,100,100)", borderRadius: "10px" }}><FontAwesomeIcon icon={faKey} size="lg" className="ps-1" /></span>
                    </div>
                    <input type="password" className="form-control border border-dark" placeholder="پسورد" onChange={getpassword} value={password} style={{ height: "50px", width: "300px", borderRadius: "10px", fontSize: "14px"  }} />
                </div>



                <div className="input-group mb-3">
                    <div className="input-group-prepend border border-dark">
                        <span className="input-group-text" id="basic-addon1" style={{ backgroundColor: "rgb(255,181,0)", height: "50px", width: "50px", borderColor: "rgb(100,100,100)", borderRadius: "10px" }}><FontAwesomeIcon icon={faMobile} size="lg" className="ps-1" /></span>
                    </div>
                        <input type="text" className="form-control border border-dark" placeholder="موبایل" onChange={getmobile} value={mobile} style={{ height: "50px", width: "300px", borderRadius: "10px", fontSize: "14px" }} />
                </div>



         

                <div className="input-group mb-3">
                    <div className="input-group-prepend">
                        
                    </div>
                    <button type="submit" className="form-control border border-dark" style={{ height: "50px", width: "300px", backgroundColor: "rgb(255,181,0)", borderRadius: "10px" }} onClick={(e) => {savedata(e)}}>ثبت نام</button>
                </div>

            </div>




        </div>

    )


}