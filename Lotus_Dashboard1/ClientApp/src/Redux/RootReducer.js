import { combineReducers } from 'redux';
import CountersecReducer from "./Countersec/CountersecReducer"


const RootReducer = combineReducers({

    countersec: CountersecReducer

});


export default RootReducer;