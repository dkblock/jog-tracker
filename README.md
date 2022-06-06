# JogTracker

## How to launch

1. Create .env file in the root of the project with the following content:
```text
DB_HOST=jog-tracker-db
DB_NAME=jogtracker
DB_USER=SA
DB_PASS=<YOUR_STRONG_PASSWORD_HERE>
DB_PORT=1433

IDENTITY_ADMIN_USERNAME=<YOUR_NAME_HERE>
IDENTITY_ADMIN_PASSWORD=<YOUR_STRONG_PASSWORD_HERE_MIN_4_CHARS>
IDENTITY_SECRET=<YOUR_STRONG_SECRET_HERE_MIN_10_CHARS>
IDENTITY_ACCESS_TOKEN_LIFETIME_IN_SECONDS=30
IDENTITY_REFRESH_TOKEN_LIFETIME_IN_DAYS=30

CLIENT_URL=http://localhost:3000
CLIENT_PORT=3000

SERVER_URL=http://localhost:5000
SERVER_PORT=5000
```
2. Run `docker-compose build && docker compose-up -d`.
3. Wait until the build is over.
4. The application will be available at the address `CLIENT_URL` specified earlier.
5. Sign in to the application using the specified username and password.