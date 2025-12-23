import tensorflow as tf

AUTO = tf.data.experimental.AUTOTUNE

def preprocess_image(image, label, img_size=32):
    image = tf.image.resize(image, [img_size, img_size])
    image = tf.cast(image, tf.float32) / 255.0
    return image, label

def get_image_dataset(batch_size=128, img_size=32, dataset='cifar10'):
    ds_train, ds_test = None, None
    if dataset == 'cifar10':
        (x_train, y_train), (x_test, y_test) = tf.keras.datasets.cifar10.load_data()
        ds_train = tf.data.Dataset.from_tensor_slices((x_train, y_train)).map(lambda x,y: preprocess_image(x,y,img_size)).shuffle(10000).batch(batch_size).prefetch(AUTO)
        ds_test  = tf.data.Dataset.from_tensor_slices((x_test, y_test)).map(lambda x,y: preprocess_image(x,y,img_size)).batch(batch_size).prefetch(AUTO)
    else:
        # Placeholder for ImageNet pipeline
        raise NotImplementedError("Use ImageNet pipeline for larger experiments")
    return ds_train, ds_test
