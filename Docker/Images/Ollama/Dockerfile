## Use the Ollama image as the base
#FROM ollama/ollama:latest
#
## Install necessary dependencies
#RUN apt-get update && \
#    apt-get install -y curl gnupg software-properties-common && \
#    rm -rf /var/lib/apt/lists/*
#
## Add NVIDIA GPG key
#RUN curl -fsSL https://nvidia.github.io/libnvidia-container/gpgkey | gpg --dearmor -o /usr/share/keyrings/nvidia-container-toolkit-keyring.gpg \
#      && curl -s -L https://nvidia.github.io/libnvidia-container/stable/deb/nvidia-container-toolkit.list | \
#        sed 's#deb https://#deb [signed-by=/usr/share/keyrings/nvidia-container-toolkit-keyring.gpg] https://#g' | \
#        tee /etc/apt/sources.list.d/nvidia-container-toolkit.list
#
## Add NVIDIA repository
#RUN apt-get update
#
## Install NVIDIA Container Toolkit
#RUN apt-get install -y nvidia-container-toolkit && \
#    apt-get clean && \
#    rm -rf /var/lib/apt/lists/* 
#
## Expose the necessary port
#EXPOSE 11434
#
## Define any necessary environment variables, volumes, etc.
#
## Maintain the default command from the Ollama image
#CMD ["ollama", "run", "your_model_name"]


#FROM nvidia/cuda:11.8.0-devel-ubuntu22.04
#
#ARG TARGETARCH="amd64"
#ARG GOFLAGS="'-ldflags=-w -s'"
#
#WORKDIR /go/src/github.com/jmorganca/ollama
#RUN apt-get update && apt-get install -y git build-essential cmake
#ADD https://dl.google.com/go/go1.21.3.linux-$TARGETARCH.tar.gz /tmp/go1.21.3.tar.gz
#RUN mkdir -p /usr/local && tar xz -C /usr/local </tmp/go1.21.3.tar.gz
#
#COPY . .
#ENV GOARCH=$TARGETARCH
#ENV GOFLAGS=$GOFLAGS
#RUN /usr/local/go/bin/go generate ./... \
#    && /usr/local/go/bin/go build .
#
#FROM nvidia/cuda:11.8.0-runtime-ubuntu22.04
#COPY --from=0 /go/src/github.com/jmorganca/ollama/ollama /bin/ollama
#EXPOSE 11434
#ENV OLLAMA_HOST 0.0.0.0
#
#ENTRYPOINT ["/bin/ollama"]
#CMD ["serve"]