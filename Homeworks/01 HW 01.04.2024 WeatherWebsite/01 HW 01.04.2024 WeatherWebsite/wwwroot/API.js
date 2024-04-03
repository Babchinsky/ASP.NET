﻿let res;
async function SendRequest(city) {
    try {

        let API = "384d5715420a512d427b2a2ef9703f51"
        let date = new Date();
        console.log(city)
        const apiUrl = `https://api.openweathermap.org/data/2.5/weather?q=${city}&date=${date}&appid=${API}&units=metric`;



        const response = await fetch(apiUrl);

        const responseJSON = await response.json();
        console.log(responseJSON);
        res = responseJSON;

        return responseJSON;

    }
    catch (error) {
        console.log(error)
    }

}
async function SendForecast(city) {

    try {

        let API = "384d5715420a512d427b2a2ef9703f51"
        let date = new Date();
        console.log(city)
        const apiUrl = `https://api.openweathermap.org/data/2.5/forecast?q=${city}&date=${date}&appid=${API}&units=metric`;



        const response = await fetch(apiUrl);

        const responseJSON = await response.json();
        console.log(responseJSON);
        res = responseJSON;

        return responseJSON;

    }
    catch (error) {
        console.log(error)
    }

}
export { SendRequest, res, SendForecast }