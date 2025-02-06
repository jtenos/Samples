<!DOCTYPE html>
<html>
    <head>
        <title>PHP Test</title>
    </head>
    <body>
        <?php 
        
        class Foo
        {
            public $Msg;
            public function __construct($msg)
            {
                $this->Msg = $msg;
            }
        }
                
        $foo = new Foo('world');
        echo '<p>Hello ' . $foo->Msg . '!';

        ?>
    </body>
</html>
