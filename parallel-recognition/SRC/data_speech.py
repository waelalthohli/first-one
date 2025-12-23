import tensorflow as tf
import tensorflow_datasets as tfds

AUTO = tf.data.experimental.AUTOTUNE

def preprocess_wav(example, sample_rate=16000, duration=1.0):
    # example: {'audio': {'array':..., 'sampling_rate':...}, 'label': ...}
    audio = example['audio']
    label = example['label']
    waveform = tf.cast(audio, tf.float32)
    # pad or trim to fixed length
    target_len = int(sample_rate * duration)
    waveform = waveform[:target_len]
    paddings = [[0, tf.maximum(0, target_len - tf.shape(waveform)[0])]]
    waveform = tf.pad(waveform, paddings)
    return waveform, label

def get_speech_dataset(name='speech_commands', batch_size=128, duration=1.0):
    ds_train = tfds.load(name, split='train', as_supervised=False)
    ds_test  = tfds.load(name, split='test', as_supervised=False)
    ds_train = ds_train.map(lambda ex: preprocess_wav(ex, duration=duration)).shuffle(10000).batch(batch_size).prefetch(AUTO)
    ds_test = ds_test.map(lambda ex: preprocess_wav(ex, duration=duration)).batch(batch_size).prefetch(AUTO)
    return ds_train, ds_test
