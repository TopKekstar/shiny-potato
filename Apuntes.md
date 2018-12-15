# Apuntes Practica 3 Aplicaciones Moviles.
Comenzamos haciendo la bola, los cubos basicos y comprobando que rebote.
*Hay que meterle material a la pelota para que al rebotar no pierda velocidad*

El cubo tiene su script, su collider y el texto. *Utilizar un Canvas3D es buena
idea para ajustarlo bien*

La pelota debería tener un metodo <code>Launch(int dirx, int diry)</code> al cual se lamaría
desde fuera.
Desarrollamos la velocidad, movimiento y rebote de la pelota. Después, que el cubo,
al ser tocado, reduzca las vidas y si tiene 0, se muera.

### Disparador
Cuando tenemos una bola que rebota, es el momento de tener un disparador. Un objeto
en la escena que lanza las N bolas en la escena.

Se generarán bolas con <code>Instantiate</code>
Esto **no** se debe hacer con Invoke. Está bien usarlo para pruebas pero no es serio ni buena Practica.
Esto se puede hacer con el fixed update. El componente estaría desactivado y se activaría solo para generar
las bolas y luego se desactivaría.
Esto se debería hacer con **corutinas**.


#### Delegados
Son funciones que se pasan como parametro o se almacenan en una variable.

Cuando la pelota toca el deathzone, se llama a la función ``goTo(Position x, delegate toExecute)`` que comienza la corrutina de movimiento y cuando acaba llama a la función delegada.
Y es el manager al ser llamado el que realiza la lógica de posicionado.

#### Aspect Ratio
Para no andar con dolores de cabeza sobre el ancho y el alto, habrá que fijar uno de los dos valores (*creo que ancho*) y que el juego calcule el alto respecto a ello.

Para ello habrá que usar el Scaler de Unity.

El tablero es de tamaño 11,25 x 14.
A los lados del mismo se generan unas imagenes pensadas para que al escalarlo en las distintas pantallas no haya huecos.

Esto hay que programarlo nosotros.
Para ello hay que tener en cuenta varias cosas:
- Tener acceso a los canvas de arriba y abajo del area de juego, para acceder a su alto. De esta manera sabemos si hay un hueco que llenar por encima
- Hacer la regla de 3 con el aspect ratio de la pantalla.
- Si el aspect ratio de la seccion de juego es distinto del de la pantalla habrá que hacer un ajuste por los laterales o por los verticales


#### Camara
