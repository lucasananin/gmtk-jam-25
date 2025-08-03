using UnityEngine;

public class LinkSelector : MonoBehaviour
{
    [Header("Link Toggle")]
    public bool openYouTube = true;

    private string youtubeLink = "https://www.youtube.com/@StudioFractured";
    private string discordLink = "https://discord.gg/vC9gcguEYN";

    public void OpenAssignedLink()
    {
        string targetLink = openYouTube ? youtubeLink : discordLink;
        Application.OpenURL(targetLink);
    }
}
