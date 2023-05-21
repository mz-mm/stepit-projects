#ifndef SEA_BATTLE_SHIP_H
#define SEA_BATTLE_SHIP_H

enum ShipsTypes {
    Carrier = 0,
    Battleship = 1,
    Cruiser = 2,
    Destroyer = 3,
    Submarine = 4
};

struct Ship{
    char* name{};
    int size{};
    int count{};
};

void shipInit(Ship* ships);

#endif
