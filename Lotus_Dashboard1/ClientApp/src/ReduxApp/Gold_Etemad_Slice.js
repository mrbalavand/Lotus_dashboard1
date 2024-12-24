import { createSlice, createAsyncThunk } from '@reduxjs/toolkit'
import axios from "axios";
//import { useContext } from 'react';
//import AppContext from '../components/AppContext.jsx';







export const GoldEtemadSlice = createAsyncThunk(
    

    'Gold_Etemad',
    async ([url,cdate,e]) => {
        /*const myContext = useContext(AppContext);*/
        let response = []
        /*const url = myContext.url;*/
        
        response = await axios.get(`${url}api/GoldEtemad_MostOnlineOrders?cdate=${cdate}&dsname=${e}`);


        console.log(response.data)

        return response.data




    }
)

const initialState = {
    entities: [],
    loading: 'idle',
}

export const usersSlice = createSlice({
    name: 'Gold_Etemad',
    initialState: initialState,
    reducers: {
        // standard reducer logic, with auto-generated action types per reducer
    },
    extraReducers: (builder) => {
        // Add reducers for additional action types here, and handle loading state as needed
        builder.addCase(GoldEtemadSlice.fulfilled, (state, action) => {
            // Add user to the state array
            state.entities = action.payload
            console.log(action)
            console.log(state)
        })
    },
})


export default usersSlice.reducer;