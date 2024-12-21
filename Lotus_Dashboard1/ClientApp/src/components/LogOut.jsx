import React, { useState, useEffect } from 'react';

import { useNavigate } from 'react-router-dom';


export function LogOut() {

    let history = useNavigate();
    useEffect(() => {

        log1()
        

    }, [])


    async function log1() {
        await localStorage.clear();
        history("/LoginUser");

    }
    return (
        
       <div></div>
        
        )


}
