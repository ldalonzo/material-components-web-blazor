namespace Leonardo.AspNetCore.Components.Material.Button
{
    public enum MDCButtonStyle
    {
        /// <summary>
        /// Text buttons are typically used for less-pronounced actions, including those located in dialogs and cards.
        /// In cards, text buttons help maintain an emphasis on card content.
        /// </summary>
        Text,

        /// <summary>
        /// Contained buttons are high-emphasis, distinguished by their use of elevation and fill.
        /// They contain actions that are primary to your app.
        /// </summary>
        Raised,

        /// <summary>
        /// Style a contained button flush with the surface.
        /// </summary>
        Unelevated,

        /// <summary>
        /// Outlined buttons are medium-emphasis buttons.
        /// They contain actions that are important, but aren't the primary action in an app.
        /// </summary>
        Outlined
    }
}
