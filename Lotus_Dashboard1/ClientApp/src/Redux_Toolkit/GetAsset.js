import { createSlice, createAsyncThunk } from '@reduxjs/toolkit';
import axios from 'axios';
import { useContext } from 'react';
import AppContext from '../components/AppContext';



export const getAssets = createAsyncThunk(

    'Asset',
    async (nationalcode) => {
        const myContext = useContext(AppContext);
        let maindata = []
        
        const response = await axios.get(`${myContext.url}/api/DDN_Order_Saham_UI?enddate=${nationalcode}`);

        if (response.data.length>0) {
            maindata = response.data;
        }
        return maindata


    }
)

const initialState = {
    entities: [],
    loading: 'idle',
}


export const usersSlice = createSlice({
    name: 'Asset',
    initialState: initialState,
    reducers: {
        // standard reducer logic, with auto-generated action types per reducer
    },
    extraReducers: (builder) => {
        // Add reducers for additional action types here, and handle loading state as needed
        builder.addCase(getAssets.fulfilled, (state, action) => {
            // Add user to the state array
            state.entities = action.payload
            console.log(action)
            console.log(state)
        })
    },
})

export default usersSlice.reducer;