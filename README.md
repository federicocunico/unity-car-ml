# unity-car-ml
Simple project for train a Car drive agent using MLAgents in Unity.

## Requirements:
- Unity
- CUDA 10.x / 11.x
- Python3.6+
    - mlagents
    - pytorch (accordingly to CUDA's version previously installed. see [pytorch website](https://pytorch.org/get-started/locally/))

## Start training:

For more details check [official docs](https://github.com/Unity-Technologies/ml-agents/blob/main/docs/Training-ML-Agents.md).

1. Open unity
2. In a terminal, open the project folder. If using a venv, activate python environment) [optionally if you want check `mlagents-learn --help`]
3. [BASIC RUN]
    - run `mlagents-learn --run-id=<run-identifier>`
    - play start on UNITY
4. [ADVANCED RUN]
    - create and/or edit the configuration file to customize your neural network and hyperparmeters (learning rate, epochs, optimizer's alpha/beta, etc...)
    - run `mlagents-learn <trainer-config-file> --run-id=<run-identifier>`

The results are stored in "./results"

### Inspect the training using tensorboard
- Open another terminal in the project's folder
- activate your venv
- run `tensorboard --logdir results --port 6006`
- open a browser and navigate to [localhost:6006](localhost:6006)

Note: If you don't assign a run-id identifier, mlagents-learn uses the default string, "ppo". You can delete the folders under the results directory to clear out old statistics.

