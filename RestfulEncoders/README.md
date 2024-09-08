# Running with Docker

To run this project using Docker, follow these steps:

1. Build the Docker image
    ```sh
    docker image build --build-arg GITHUB_USERNAME=?? --build-arg GITHUB_TOKEN=?? --pull --tag 'restfulencoders:latest' .
    ```
2. Run the image
    ```sh
    docker container run --rm -d -p 5140:5140/tcp restfulencoders:latest
    ```

