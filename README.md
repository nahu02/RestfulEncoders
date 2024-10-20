# Running with Docker

To run this project using Docker, follow these steps:

1. Make sure to define a `.env` file based on `.env.example`!

2. Build the Docker image
    ```sh
    docker image build --build-arg GITHUB_USERNAME=?? --build-arg GITHUB_TOKEN=?? --pull --tag 'restfulencoders:latest' .
    ```
3. Run the image
    ```sh
    docker container run --rm -d -p 5140:5140/tcp restfulencoders:latest
    ```

