import '../index.css';
import React, { useState } from 'react';

const LoginPage = ({ onLoginSuccess }) => {
  const [credentials, setCredentials] = useState({ name: '', password: '' });
  const [error, setError] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleChange = (e) => {
    setCredentials({ ...credentials, [e.target.name]: e.target.value });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setIsSubmitting(true);
    setError('');

    try {
      // POST: Send credentials to your backend (e.g., /api/login)
      const response = await fetch('/api/login', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(credentials),
      });

      if (response.ok) {
        const data = await response.json();
        // Pass the manager's data/token back to the main App
        onLoginSuccess(data.token); 
      } else {
        setError('Unauthorized Access: Invalid Manager Credentials');
      }
    } catch (err) {
      setError('Connection failed. Please check your network.');
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <div className="min-h-screen bg-[#f1f5f9] flex items-center justify-center p-4">
      <div className="max-w-md w-full bg-white rounded-xl shadow-2xl overflow-hidden border border-slate-200">
        
        {/* Visual Header from Design */}
        <div className="bg-[#1a2b4b] py-10 text-center px-6">
          <div className="inline-block p-3 rounded-full bg-blue-500/20 mb-4">
             <span className="text-blue-400 text-2xl">🔒</span>
          </div>
          <h2 className="text-white text-xs uppercase tracking-[0.3em] font-bold opacity-70">Secure Admin Gate</h2>
          <h1 className="text-white text-2xl font-light mt-1 uppercase">Manager Access Portal</h1>
        </div>

        <div className="p-8">
          {error && (
            <div className="mb-6 p-3 bg-red-50 border-l-4 border-red-500 text-red-700 text-sm">
              {error}
            </div>
          )}

          <form onSubmit={handleSubmit} className="space-y-5">
            <div>
              <label className="block text-[11px] font-bold text-slate-500 uppercase mb-2">Manager Name</label>
              <input 
                type="text" 
                name="name"
                value={credentials.name}
                required
                className="w-full px-4 py-3 bg-slate-50 border border-slate-200 rounded-lg focus:ring-2 focus:ring-blue-500 outline-none transition-all text-slate-700"
                placeholder="manager_name"
                onChange={handleChange}
              />
            </div>

            <div>
              <label className="block text-[11px] font-bold text-slate-500 uppercase mb-2">Secret Passkey</label>
              <input 
                type="password" 
                name="password"
                value={credentials.password}
                required
                className="w-full px-4 py-3 bg-slate-50 border border-slate-200 rounded-lg focus:ring-2 focus:ring-blue-500 outline-none transition-all text-slate-700"
                placeholder="••••••••"
                onChange={handleChange}
              />
            </div>

            <button 
              type="submit"
              disabled={isSubmitting}
              className="w-full bg-[#1e88e5] hover:bg-blue-600 text-white font-bold py-4 rounded-lg transition-all shadow-lg shadow-blue-500/20 active:scale-[0.98]"
            >
              {isSubmitting ? 'VERIFYING...' : 'ACCESS DASHBOARD'}
            </button>
          </form>

          <div className="mt-8 text-center border-t border-slate-100 pt-6">
            <p className="text-slate-400 text-sm">
              Register New Account <span className="text-blue-500 font-medium cursor-not-allowed italic">(Invite Only)</span>
            </p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default LoginPage;