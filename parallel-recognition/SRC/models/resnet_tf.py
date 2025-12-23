import tensorflow as tf

def build_resnet(input_shape=(32,32,3), num_classes=10):
    base = tf.keras.applications.ResNet50(weights=None, include_top=False, input_shape=input_shape)
    x = tf.keras.layers.GlobalAveragePooling2D()(base.output)
    out = tf.keras.layers.Dense(num_classes, activation='softmax')(x)
    model = tf.keras.Model(inputs=base.input, outputs=out)
    return model
