import React from 'react';
import { useState } from 'react';
import axios from 'axios';
import { useContext } from 'react';
import AppContext from './AppContext';
import { NavMenu } from './NavMenu';
import { Slider } from './Slider';
import { Footer } from './Footer';

import Dropdown from 'react-bootstrap/Dropdown';

export function Customerchart() {

    let [assets, setassets] = useState("");

    let [fund, setfundname] = useState("");


    

    let myContext = useContext(AppContext);

    async function callassets(e) {


        console.log(e)
        e.preventDefault();
        var parameters = [{ Name: "nationalcode", Value: assets }, { Name: "dscode", Value: fund }];
        var data = await axios.post(myContext.url + "api/PositionAssets",parameters);


    }


    async function fundcode(e) {

        console.log(e)
        e.preventDefault();




    }

    return (
        

        <div>

            
            <div className="pe-4 ps-4" ><NavMenu /></div>
            <div className="row d-flex m-0">
                <div className="col-md-2 ps-4 justify-content-center"><Slider /></div>
                <div className="col-md-10 d-flex ps-4 pt-4 pe-4 justify-content-center">

                    <form style={{ width: "50%" }}>
                        {/*<hr style={{ backgroundColor: "rgb(255, 181, 0)" }} />*/}
                        <div className="mb-3 mt-3">

                            <label className="form-label d-block text-center" style={{fontSize:"18px"} }>کدملی سرمایه گذار</label>
                            <input type="text" style={{ textAlign: "center" }} className="form-control" id="exampleInputEmail1" onChange={(e) => {setassets(e.target.value) } }/>
                                <div id="emailHelp" className="form-text">به منظور نمایش داشبورد سرمایه گذار، لطفا کدملی را در کادر بالا وارد نمایید.</div>
                        </div>

                        <label className="form-label d-block text-center" style={{ fontSize: "18px" }}>نام صندوق</label>
                        <Dropdown className="mb-3 d-block d-flex justify-content-center" style={{width:"100%"} }>
                            <Dropdown.Toggle variant="success" id="dropdown-basic" className="mb-5" style={{ width: "40%", backgroundColor:"rgb(72,122,123)",color:"white" }}>
                                {fund.length === 0 ? "لطفا یک صندوق را انتخاب نمایید" : fund}
                            </Dropdown.Toggle>

                            <Dropdown.Menu>
                                <Dropdown.Item onClick={(e) => { setfundname("صندوق لوتوس") } }>صندوق لوتوس</Dropdown.Item>
                                <Dropdown.Item onClick={(e) => { setfundname("صندوق پیروزان") }}>صندوق پیروزان</Dropdown.Item>
                                <Dropdown.Item onClick={(e) => { setfundname("صندوق زرین") }}>صندوق زرین</Dropdown.Item>
                                <Dropdown.Item onClick={(e) => { setfundname("صندوق رویان") }}>صندوق رویان</Dropdown.Item>
                                <Dropdown.Item onClick={(e) => { setfundname("صندوق الزهرا") }}>صندوق الزهرا</Dropdown.Item>
                            </Dropdown.Menu>
                        </Dropdown>
                        <hr style={{ backgroundColor: "rgb(72,122,123)" }} className="shadow" />
                        
                        <button type="submit" className="btn btn-primary shadow col-md-4 offset-md-4 mt-5 mb-5" style={{ backgroundColor: "rgb(255, 181, 0)", color: "rgb(33, 37, 41)", heigh: "100%" }} onClick={(e) => { callassets(e) }}>نمایش اطلاعات</button>
                        
                    </form>

                    
                </div>

            </div>

            <div className="pe-4 ps-4"><Footer /></div>


        </div>
    )
}