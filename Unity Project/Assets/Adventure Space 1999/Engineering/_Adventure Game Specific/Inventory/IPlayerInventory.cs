using UnityEngine;
using System.Collections.Generic;

namespace WhatPumpkin
{

    public interface IPlayerInventory
    {
        /// <summary>
        /// Does the player have the item of the specified key name
        /// </summary>
        /// <param name="itemKey"></param>
        /// <returns></returns>

        bool HasItem(string itemKey);

        /// <summary>
        /// Does the user have each of these items
        /// </summary>
        /// <param name="_items"></param>
        /// <returns></returns>

        bool HasItems(List<string> _itemKeys);

        /// <summary>
        /// Get a list of all items
        /// </summary>

        List<IItem> Items { get; }

        /// <summary>
        /// Adds an item to the next empty slot
        /// </summary>
        /// <param name="item"></param>

        void AddItemToNextEmpty(IItem item);
    }
}
