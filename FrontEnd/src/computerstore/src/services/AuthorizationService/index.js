const Host = "http://localhost:25648";

export const SignIn = async (userName, password) => {
  const response = await fetch(`${Host}/api/User/login`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({ userName, password }),
  });

  const data = await response.json();

  window.localStorage.setItem("token", data.token);
};

export const SignOut = () => {
  window.localStorage.setItem("token", "");
};

export const getWeatherForecast = async () => {
  const response = await fetch(`${Host}`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json",
      Authorization: "Bearer " + window.localStorage.getItem("token"),
    },
  });
};
