using System.Collections.Generic;

public static class TownController
{
    public static List<PlaceableObject> placeableObjects = new List<PlaceableObject>();

    public static List<PlaceableObject> getPlaceableObjects()
    {
        return placeableObjects;
    }

    public static int addPlaceableObject(PlaceableObject placeableObject)
    {
        placeableObjects.Add(placeableObject);

        return placeableObjects.Count - 1;
    }
}
