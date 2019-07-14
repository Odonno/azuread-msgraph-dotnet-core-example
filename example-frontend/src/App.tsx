import React, { useEffect, useState } from 'react';
import { getToken } from './adalConfig';
import { CurrentUser } from './models';
import { User } from '@microsoft/microsoft-graph-types';

const App: React.FC = () => {
  const [currentUser, setCurrentUser] = useState<CurrentUser>();
  const [search, setSearch] = useState('');
  const [usersResult, setUsersResult] = useState<User[]>([]);

  useEffect(() => {
    const headers = { Authorization: `Bearer ${getToken()}` };

    fetch('https://localhost:5001/api/user/current', { headers })
      .then(response => {
        if (response.ok) {
          response.json().then(u => setCurrentUser(u));
        } else {
          setCurrentUser(undefined);
        }
      });
  }, []);

  useEffect(() => {
    if (!search) {
      setUsersResult([]);
      return;
    }

    const headers = { Authorization: `Bearer ${getToken()}` };

    fetch(`https://localhost:5001/api/user/search/${search}`, { headers })
      .then(response => {
        if (response.ok) {
          response.json().then(v => setUsersResult(v));
        } else {
          setUsersResult([]);
        }
      });
  }, [search]);

  if (!currentUser || !currentUser.isAuthenticated) {
    return <div>Not authenticated</div>;
  }

  return (
    <div>
      <div>Authenticated as {currentUser.login}</div>

      <input type="search" placeholder="Search users" onChange={e => setSearch(e.target.value)} />

      {usersResult && (
        <div>
          Results:

          <ul>
            {usersResult.map(u =>
              <li key={u.id}>{u.displayName}</li>)
            }
          </ul>
        </div>
      )}
    </div>
  );
}

export default App;
