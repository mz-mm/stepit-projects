#include "ship.h"


void shipInit(Ship* ships) {
    ships[Carrier] = {"Carrier", 5, 1};
    ships[Battleship]= {"Battleship", 4, 1};
    ships[Cruiser] = {"Cruiser", 3, 1};
    ships[Destroyer] = {"Destoroyer", 2, 2};
    ships[Submarine]= {"Submarine", 1, 2};
}
