import React, { useState, useEffect } from 'react';
import { Route, Routes } from 'react-router-dom';

import { Home } from './components/Home';
import { LoginUser } from './components/LoginUser';
import { RegisterUser } from './components/RegisterUser';
import { LogOut } from './components/LogOut';
import { Footer } from './components/Footer';
import { FundTable } from './components/FundTable';
import AppContext from './components/AppContext';
import { Customerchart } from './components/Customerchart';
import { Selectfunds } from './components/Selectfunds';
import { Onlinedashboard } from './components/Onlinedashboard';
import { Selectfund1 } from './components/Selectfund1';
import { Mostonlineorder } from './components/Mostonlineorder';
import { CountTime } from './components/CountTime';


import { Maxunitmodal } from './components/Maxunitmodal';
import { store } from './Redux_Toolkit/store';
import { Provider } from 'react-redux';


export default function App() {

    //const [setting1value, setSetting1value] = useState({ url: "https://localhost:7037/", fundname: "" });
    const [setting1value, setSetting1value] = useState({ url: "" });

    return (

        <AppContext.Provider value={setting1value}>
            <Provider store={store}>

                <Routes>

                    <Route exact path='/' element={<Onlinedashboard />} />
                    <Route exact path='/LoginUser' element={<LoginUser />} />
                    <Route exact path='/RegisterUser' element={<RegisterUser />} />
                    <Route exact path='/LogOut' element={<LogOut />} />
                    <Route exact path='/Footer' element={<Footer />} />
                    <Route exact path='/FundTable' element={<FundTable />} />
                    <Route exact path='/Customerchart' element={<Customerchart />} />
                    <Route exact path='/Selectfunds' element={<Selectfunds />} />
                    <Route exact path='/Onlinedashboard' element={<Onlinedashboard />} />
                    <Route exact path='/Selectfund1' element={<Selectfund1 />} />
                    <Route exact path='/Mostonlineorder' element={<Mostonlineorder />} />
                    <Route exact path='/CountTime' element={<CountTime />} />
                    <Route exact path='/Maxunitmodal' element={<Maxunitmodal/>} />

                </Routes>

            </Provider>
        </AppContext.Provider>

    );

}
