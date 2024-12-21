import Decrease from "./CountsecAction";

const initialstate = {countsec:0}

const CountersecReducer = (state = initialstate,action) => {

    switch (action.type) {

        case Decrease:
            return {
                ...state,
                countsec: state.countsec + 1
            };
        default:
            return state;
    }

}

export default CountersecReducer;