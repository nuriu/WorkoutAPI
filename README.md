# Workout API
> Simple workout api developed using .net 6 and mysql


Requirements:
> docker


Run app using:
> docker compose up -d --build

If you get CS0579 errors during docker image building, delete bin and obj folder then try again:
> del **/bin && del **/obj
> docker compose up -d --build

**Note**: It can take up to 2 minutes (depending on machine it runs on) to seed data.
