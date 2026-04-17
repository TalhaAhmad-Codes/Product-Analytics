import React, { useState } from 'react';
import LoginPage from './Components/LoginPage';
import ManagerDashboard from './Components/ManagerDashboard';

function App() {
  // Store the token from the backend here
  // const [authToken, setAuthToken] = useState(localStorage.getItem('token'));

  // const handleLogin = (token) => {
  //   localStorage.setItem('token', token); // Save session
  //   setAuthToken(token);
  // };

  // const handleLogout = () => {
  //   localStorage.removeItem('token');
  //   setAuthToken(null);
  // };

  // return (
  //   <div className="App">
  //     {authToken ? (
  //       <ManagerDashboard onLogout={handleLogout} />
  //     ) : (
  //       <LoginPage onLoginSuccess={handleLogin} />
  //     )}
  //   </div>
  // );

  return (
    <ManagerDashboard />
  );
}

export default App;
