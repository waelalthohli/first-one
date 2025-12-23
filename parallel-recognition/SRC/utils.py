import time, os, json
import tensorflow as tf

def save_config(cfg, path):
    with open(path, 'w') as f:
        json.dump(cfg, f, indent=2)

def now_ms():
    return int(time.time() * 1000)

def print_devices():
    print("Physical GPUs:", tf.config.list_physical_devices('GPU'))
    print("Logical GPUs:", tf.config.list_logical_devices('GPU'))
    print("TPU devices:", tf.config.list_logical_devices('TPU'))
