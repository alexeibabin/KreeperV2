private var sound : AudioSource;

function Start()
{
    sound = GetComponent (AudioSource);
}

function Play ()
{
    sound.Play();
}
