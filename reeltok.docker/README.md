# The ReelTok Docker

In order to setup The ReelTok docker composition you'll need to do the following:

- Inside `config/` you need to insert the following appsettings.json files for production use:
  
  <table>
    <thead>
        <td>
            API
        </td>
        <td>
            config/{Filename}
        </td>
    </thead>
    <tbody>
        <tr>
            <td>
                Gateway API
            </td>
            <td>
                appsettings.gateway.json
            </td>
        </tr>
        <tr>
            <td>
                Users API
            </td>
            <td>
                appsettings.users.json
            </td>
        </tr>
        <tr>
            <td>
                Videos API
            </td>
            <td>
                appsettings.videos.json
            </td>
        </tr>
        <tr>
            <td>
                Auth API
            </td>
            <td>
                appsettings.auth.json
            </td>
        </tr>
        <tr>
            <td>
                Recommendations Api
            </td>
            <td>
                appsettings.recommendations.json
            </td>
        </tr>
                <tr>
            <td>
                Comments API
            </td>
            <td>
                appsettings.comments.json
            </td>
        </tr>
    </tbody>
  </table>
- Next you need to create `.env` file using the template included in the reeltok.docker directory root and fill out all the variables
- Next create a folder inside the reeltok.docker directory root and call it `mssql-data` and chown it recuresively using `chown -R 10001:10001 mssql-data/` so that the mssql user owns it
- Next create a network for the `Traefik Service` using `docker network create traefik`
- Next make sure to check if any containers (using `docker ps`) or services on your system are already listening on any of the ports exposed inside `docker-compose.yml`
- If everything is good and all the conditions have been fulfiled you should be able to run `docker-compose up -d ` and watch the containers build and spin up without any problems

$${\color{yellow}NOTE:}$$ Be careful with what kind of password you set in the `.env` variables for the `SFTP` as digits might cause parsing issues in the `atmoz/sftp` parsing logic and in turn mess up API authentication for those which use SFTP 

